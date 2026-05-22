using ScreenRecorderLib;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    public partial class ScreenRecorder : Form
    {
        #region Fields

        private Recorder _recorder;
        private bool _isStoppingRecording;
        private bool _isClosingAfterRecordingStop;
        private bool _isCountdownActive;
        private Task _currentStopTask = Task.CompletedTask;
        private readonly object _stopSync = new object();
        private readonly object _recordingActionCooldownSync = new object();
        private DateTime _recordingStartTime;
        private DateTime? _recordingPausedAt;
        private DateTime _nextStartAllowedUtc = DateTime.MinValue;
        private DateTime _nextStopAllowedUtc = DateTime.MinValue;
        private Timer _cooldownStatusRestoreTimer;
        private string _pendingCooldownStatusMessage;
        private const int MinRegionWidth = 100;
        private const int MinRegionHeight = 100;
        private static readonly TimeSpan RecordingActionCooldown = TimeSpan.FromSeconds(3);

        #endregion

        #region Start / Stop

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            await HandlePrimaryRecordingActionAsync(sender, e);
        }

        private async Task HandlePrimaryRecordingActionAsync(object sender, EventArgs e)
        {
            if (_isStoppingRecording)
                return;
            if (_isCountdownActive)
                return;

            if (_recorder != null)
            {
                if (_recorder.Status == RecorderStatus.Recording)
                {
                    await PauseRecordingAsync();
                    return;
                }

                if (_recorder.Status == RecorderStatus.Paused)
                {
                    ResumeRecording();
                    return;
                }

                ShowTemporaryCooldownStatus("Status: Please wait...");
                return;
            }

            await StartRecordingAsync(sender, e);
        }

        private async Task StartRecordingAsync(object sender, EventArgs e)
        {
            if (_isStoppingRecording)
                return;
            if (_isCountdownActive)
                return;

            if (_recorder != null)
            {
                if (_recorder.Status == RecorderStatus.Recording)
                {
                    MessageBox.Show("Recording is already in progress.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    ShowTemporaryCooldownStatus("Status: Please wait...");
                }
                return;
            }

            if (!CanStartRecordingNow())
            {
                ShowTemporaryCooldownStatus("Status: Please wait before starting again...");
                return;
            }

            try
            {
                _outputPath = ResolveOutputPath(txtPath.Text);
                lblStatus.Text = "Status: Preparing...";
                SetControlsEnabled(false);

                if (countdownChkBox.Checked)
                {
                    _isCountdownActive = true;
                    for (int i = 3; i >= 1; i--)
                    {
                        if (IsDisposed || Disposing)
                        {
                            _isCountdownActive = false;
                            return;
                        }

                        lblStatus.Text = "Status: Recording starts in " + i + "...";
                        await Task.Delay(1000);
                    }
                    _isCountdownActive = false;

                    if (IsDisposed || Disposing)
                    {
                        _isCountdownActive = false;
                        return;
                    }

                    lblStatus.Text = "Status: Preparing...";
                }

                if (!TryValidateOutputDirectory(_outputPath, out string outputValidationError))
                {
                    lblStatus.Text = "Status: Idle";
                    SetControlsEnabled(true);
                    MessageBox.Show(outputValidationError, "Invalid Output Folder",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var options = GetSelectedMonitorOptions();

                bool recordSystemAudio = cbRecordSystemAudio.Checked;
                bool useMic = comboBoxMic.SelectedIndex > 0;
                string audioDeviceName = string.Empty;

                if (useMic && !_inputDevicesMap.ContainsKey(Convert.ToString(comboBoxMic.SelectedItem)))
                {
                    MessageBox.Show("Selected microphone is not available. Defaulting to None.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBoxMic.SelectedIndex = 0;
                    useMic = false;
                }

                if (useMic)
                {
                    string friendlyName = Convert.ToString(comboBoxMic.SelectedItem);
                    if (!string.IsNullOrEmpty(friendlyName) && _inputDevicesMap.ContainsKey(friendlyName))
                        audioDeviceName = _inputDevicesMap[friendlyName];
                }

                options.AudioOptions = new AudioOptions
                {
                    IsAudioEnabled = useMic || recordSystemAudio,
                    IsInputDeviceEnabled = useMic,
                    AudioInputDevice = audioDeviceName,
                    IsOutputDeviceEnabled = recordSystemAudio,
                    Channels = AudioChannels.Stereo,
                    Bitrate = AudioBitrate.bitrate_192kbps,
                    InputVolume = 1.0f
                };

                string selectedCodec = Convert.ToString(comboBoxCodec.SelectedItem);
                if (string.IsNullOrEmpty(selectedCodec))
                {
                    selectedCodec = "H.264 (AVC)";
                }

                options.VideoEncoderOptions = new VideoEncoderOptions
                {
                    Framerate = GetSelectedFrameRate(),
                    Bitrate = GetBitrateByQuality(trackBarQuality.Value),
                    IsFixedFramerate = true,
                    IsHardwareEncodingEnabled = true,
                    Encoder = GetVideoEncoder(selectedCodec)
                };

                await Task.Run(() =>
                {
                    _recorder = Recorder.CreateRecorder(options);
                    SubscribeRecorderEvents(_recorder);

                    _recorder.Record(_outputPath);
                });

                _recordingStartTime = DateTime.Now;
                _recordingPausedAt = null;
                lblRecordingTimer.Text = "00:00:00";
                recordingTimer.Start();
                SetNextStopAllowedUtc(DateTime.UtcNow.Add(RecordingActionCooldown));
                ApplyRecordingUi();
            }
            catch (Exception ex)
            {
                _isCountdownActive = false;
                StopAndResetRecordingTimer();
                _recordingPausedAt = null;
                StopTrayRecordingIndicator();
                if (_recorder != null)
                {
                    UnsubscribeRecorderEvents(_recorder);
                    _recorder.Dispose();
                    _recorder = null;
                }
                _outputPath = null;

                MessageBox.Show("Error starting recording:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetControlsEnabled(true);
            }
        }

        private async Task StopRecording(object sender, EventArgs e)
        {
            try
            {
                if (_isStoppingRecording)
                {
                    await StopAndDisposeRecorderAsync();
                    return;
                }
                if (_isCountdownActive)
                {
                    return;
                }

                if (_recorder == null)
                {
                    lblStatus.Text = "Status: Idle";
                    return;
                }

                if (!CanStopRecordingNow())
                {
                    ShowTemporaryCooldownStatus("Status: Please wait before stopping...");
                    return;
                }

                await StopAndDisposeRecorderAsync();
                if (!_isClosingAfterRecordingStop)
                {
                    lblStatus.Text = "Status: Stopped";
                    SetControlsEnabled(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error stopping recording:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (_recorder != null)
                {
                    UnsubscribeRecorderEvents(_recorder);
                    _recorder.Dispose();
                    _recorder = null;
                }
                _outputPath = null;
                _recordingPausedAt = null;
                StopAndResetRecordingTimer();
                StopTrayRecordingIndicator();
                FlashLabel(lblStatus, false);
                SetNextStartAllowedUtc(DateTime.UtcNow.Add(RecordingActionCooldown));
                SetControlsEnabled(true);
            }
        }

        private async void BtnStop_Click(object sender, EventArgs e)
        {
            await StopRecording(sender, e);
        }

        private async Task PauseRecordingAsync()
        {
            Recorder recorder = _recorder;
            if (recorder == null || recorder.Status != RecorderStatus.Recording)
                return;

            try
            {
                await Task.Run(() => recorder.Pause());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error pausing recording:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResumeRecording()
        {
            Recorder recorder = _recorder;
            if (recorder == null || recorder.Status != RecorderStatus.Paused)
                return;

            try
            {
                recorder.Resume();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error resuming recording:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CanStartRecordingNow()
        {
            lock (_recordingActionCooldownSync)
            {
                return DateTime.UtcNow >= _nextStartAllowedUtc;
            }
        }

        private bool CanStopRecordingNow()
        {
            lock (_recordingActionCooldownSync)
            {
                return DateTime.UtcNow >= _nextStopAllowedUtc;
            }
        }

        private void SetNextStartAllowedUtc(DateTime nextAllowedUtc)
        {
            lock (_recordingActionCooldownSync)
            {
                _nextStartAllowedUtc = nextAllowedUtc;
            }
        }

        private void SetNextStopAllowedUtc(DateTime nextAllowedUtc)
        {
            lock (_recordingActionCooldownSync)
            {
                _nextStopAllowedUtc = nextAllowedUtc;
            }
        }

        private void ShowTemporaryCooldownStatus(string message)
        {
            if (IsDisposed || Disposing || !IsHandleCreated)
                return;

            EnsureCooldownStatusRestoreTimer();

            _pendingCooldownStatusMessage = message;
            lblStatus.Text = message;

            if (_cooldownStatusRestoreTimer == null)
                return;

            _cooldownStatusRestoreTimer.Stop();
            _cooldownStatusRestoreTimer.Interval = (int)RecordingActionCooldown.TotalMilliseconds;
            _cooldownStatusRestoreTimer.Start();
        }

        private void RestoreRecordingStatusText()
        {
            if (_isCountdownActive)
                return;

            if (_isStoppingRecording)
            {
                lblStatus.Text = "Status: Stopping...";
                return;
            }

            if (_recorder != null && _recorder.Status == RecorderStatus.Paused)
            {
                lblStatus.Text = "Status: Paused";
                return;
            }

            if (_recorder != null && _recorder.Status == RecorderStatus.Recording)
            {
                lblStatus.Text = _selectedRecordingRegion.HasValue
                    ? "Status: Recording selected area..."
                    : "Status: Recording...";
                return;
            }

            lblStatus.Text = btnStop.Enabled ? "Status: Stopped" : "Status: Idle";
        }

        private void EnsureCooldownStatusRestoreTimer()
        {
            if (_cooldownStatusRestoreTimer != null || components == null)
                return;

            _cooldownStatusRestoreTimer = new Timer(components);
            _cooldownStatusRestoreTimer.Tick += CooldownStatusRestoreTimer_Tick;
        }

        private void CooldownStatusRestoreTimer_Tick(object sender, EventArgs e)
        {
            if (_cooldownStatusRestoreTimer != null)
                _cooldownStatusRestoreTimer.Stop();

            if (IsDisposed || Disposing || !IsHandleCreated)
                return;

            string pendingMessage = _pendingCooldownStatusMessage;
            _pendingCooldownStatusMessage = null;

            if (!string.IsNullOrEmpty(pendingMessage) && lblStatus.Text == pendingMessage)
                RestoreRecordingStatusText();
        }

        private void ApplyRecordingUi()
        {
            if (_recordingPausedAt.HasValue)
            {
                _recordingStartTime = _recordingStartTime.Add(DateTime.Now - _recordingPausedAt.Value);
                _recordingPausedAt = null;
            }

            if (recordingTimer != null && !recordingTimer.Enabled)
                recordingTimer.Start();

            StartTrayRecordingIndicator();
            SetControlsEnabled(false);
            lblStatus.Text = _selectedRecordingRegion.HasValue
                ? "Status: Recording selected area..."
                : "Status: Recording...";
            FlashLabel(lblStatus, true);
        }

        private void ApplyPausedUi()
        {
            if (!_recordingPausedAt.HasValue)
                _recordingPausedAt = DateTime.Now;

            if (recordingTimer != null)
                recordingTimer.Stop();

            ShowPausedTrayIndicator();
            SetControlsEnabled(false);
            lblStatus.Text = "Status: Paused";
            FlashLabel(lblStatus, false);
        }

        #endregion

        #region Recorder Events

        private void SubscribeRecorderEvents(Recorder recorder)
        {
            if (recorder == null) return;
            recorder.OnRecordingFailed += Recorder_OnRecordingFailed;
            recorder.OnRecordingComplete += Recorder_OnRecordingComplete;
            recorder.OnStatusChanged += Recorder_OnStatusChanged;
        }

        private void UnsubscribeRecorderEvents(Recorder recorder)
        {
            if (recorder == null) return;
            recorder.OnRecordingFailed -= Recorder_OnRecordingFailed;
            recorder.OnRecordingComplete -= Recorder_OnRecordingComplete;
            recorder.OnStatusChanged -= Recorder_OnStatusChanged;
        }

        private void Recorder_OnRecordingFailed(object sender, RecordingFailedEventArgs evt)
        {
            if (IsHandleCreated && !IsDisposed)
                BeginInvoke((MethodInvoker)(() =>
                {
                    StopAndResetRecordingTimer();
                    MessageBox.Show("Recording failed: " + evt.Error, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FlashLabel(lblStatus, false);
                    _recordingPausedAt = null;
                    StopTrayRecordingIndicator();
                    lblStatus.Text = "Status: Error";
                    _outputPath = null;
                    SetControlsEnabled(true);
                    _ = StopAndDisposeRecorderAsync();
                }));
        }

        private void Recorder_OnRecordingComplete(object sender, RecordingCompleteEventArgs evt)
        {
            if (IsHandleCreated && !IsDisposed)
                BeginInvoke((MethodInvoker)(() =>
                {
                    try
                    {
                        StopAndResetRecordingTimer();
                        _recordingPausedAt = null;
                        FlashLabel(lblStatus, false);
                        StopTrayRecordingIndicator();
                        lblStatus.Text = "Status: Saved";
                        SetControlsEnabled(true);
                        _lastCompletedRecordingFile = evt.FilePath;
                        string recordingFolder = Path.GetDirectoryName(evt.FilePath) ?? _videosFolder;
                        txtPath.Text = recordingFolder;
                        _lastCompletedRecordingFolder = recordingFolder;
                        openLastRecordingToolStripMenuItem.Enabled = File.Exists(_lastCompletedRecordingFile);
                        _outputPath = null;

                        if (notifyIcon.Visible)
                        {
                            _isCompletionBalloonActive = true;
                            notifyIcon.BalloonTipTitle = "Recording Complete";
                            notifyIcon.BalloonTipText = "Recording saved to:\n" + evt.FilePath;
                            notifyIcon.ShowBalloonTip(6000);
                        }
                        else
                        {
                            MessageBox.Show("Recording saved:\n" + evt.FilePath,
                                "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show("Error after saving recording:\n" + ex2.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }));
        }

        private void Recorder_OnStatusChanged(object sender, RecordingStatusEventArgs evt)
        {
            if (IsHandleCreated && !IsDisposed)
                BeginInvoke((MethodInvoker)(() =>
                {
                    RecorderStatus status = (RecorderStatus)evt.Status;

                    if (status == RecorderStatus.Paused)
                    {
                        ApplyPausedUi();
                        return;
                    }

                    if (status == RecorderStatus.Recording)
                    {
                        ApplyRecordingUi();
                    }
                    else
                    {
                        lblStatus.Text = "Status: " + status;
                    }

                    if (status == RecorderStatus.Recording && hideonrecordChkBox.Checked)
                    {
                        WindowState = FormWindowState.Minimized;
                        MinimizeApp();
                    }
                }));
        }

        private Task StopAndDisposeRecorderAsync()
        {
            lock (_stopSync)
            {
                if (_isStoppingRecording)
                    return _currentStopTask ?? Task.CompletedTask;

                _isStoppingRecording = true;
                _currentStopTask = StopAndDisposeRecorderCoreAsync();
                return _currentStopTask;
            }
        }

        private async Task StopAndDisposeRecorderCoreAsync()
        {
            Recorder recorder = _recorder;
            if (recorder == null)
            {
                lock (_stopSync)
                {
                    _isStoppingRecording = false;
                    _currentStopTask = Task.CompletedTask;
                }
                return;
            }

            EventHandler<RecordingCompleteEventArgs> onComplete = null;

            try
            {
                if (recorder.Status == RecorderStatus.Recording || recorder.Status == RecorderStatus.Paused)
                {
                    var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

                    onComplete = (s, e) => { tcs.TrySetResult(true); };

                    recorder.OnRecordingComplete += onComplete;
                    recorder.Stop();

                    Task completedTask = await Task.WhenAny(tcs.Task, Task.Delay(TimeSpan.FromSeconds(10)));
                    if (completedTask != tcs.Task)
                    {
                        Debug.WriteLine("Timed out waiting for OnRecordingComplete while stopping recorder.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error stopping/disposing recorder: " + ex.Message);
            }
            finally
            {
                if (onComplete != null)
                {
                    try
                    {
                        recorder.OnRecordingComplete -= onComplete;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error unsubscribing temporary completion handler: " + ex.Message);
                    }
                }

                try
                {
                    UnsubscribeRecorderEvents(recorder);
                    recorder.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error disposing recorder: " + ex.Message);
                }

                _recorder = null;
                _outputPath = null;
                _recordingPausedAt = null;
                StopAndResetRecordingTimer();
                StopTrayRecordingIndicator();
                FlashLabel(lblStatus, false);
                SetNextStartAllowedUtc(DateTime.UtcNow.Add(RecordingActionCooldown));

                if (!_isClosingAfterRecordingStop && IsHandleCreated && !IsDisposed)
                {
                    if (InvokeRequired)
                    {
                        BeginInvoke((MethodInvoker)(() => SetControlsEnabled(true)));
                    }
                    else
                    {
                        SetControlsEnabled(true);
                    }
                }

                lock (_stopSync)
                {
                    _isStoppingRecording = false;
                    _currentStopTask = Task.CompletedTask;
                }
            }
        }

        #endregion

        #region Recorder Options

        private RecorderOptions GetSelectedMonitorOptions()
        {
            int selectedMonitorIndex = comboBoxMonitor.SelectedIndex;

            if (_availableDisplays == null || _availableDisplays.Count == 0)
            {
                throw new InvalidOperationException("No valid recording sources (monitors) were found.");
            }

            if (selectedMonitorIndex < 0 || selectedMonitorIndex >= _availableDisplays.Count)
            {
                selectedMonitorIndex = 0;
            }

            RecordableDisplay selectedDisplay = _availableDisplays[selectedMonitorIndex];
            Screen targetScreen = null;
            Rectangle clampedDesktopRegion = Rectangle.Empty;
            Rectangle localRegion = Rectangle.Empty;
            bool areaMode = TryGetUsableSelectedArea(out clampedDesktopRegion, out targetScreen, out localRegion);
            if (_selectedRecordingRegion.HasValue && !areaMode)
            {
                MessageBox.Show(
                    "Selected area is invalid or outside active screens.\nFalling back to full monitor recording.",
                    "Area Selection Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            if (areaMode && targetScreen != null)
            {
                int displayIndexForScreen = _availableDisplays.FindIndex(d =>
                    string.Equals(d.DeviceName, targetScreen.DeviceName, StringComparison.OrdinalIgnoreCase));

                if (displayIndexForScreen >= 0)
                {
                    selectedDisplay = _availableDisplays[displayIndexForScreen];
                }
                else
                {
                    MessageBox.Show(
                        "Selected area monitor could not be mapped to recording display.\nUsing currently selected monitor instead.",
                        "Area Selection Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }

            DisplayRecordingSource displaySource = new DisplayRecordingSource
            {
                DeviceName = selectedDisplay.DeviceName
            };

            if (areaMode)
            {
                displaySource.SourceRect = new ScreenRect(
                    localRegion.Left,
                    localRegion.Top,
                    localRegion.Width,
                    localRegion.Height);
            }

            var options = new RecorderOptions
            {
                SourceOptions = new SourceOptions
                {
                    RecordingSources = new System.Collections.Generic.List<RecordingSourceBase> { displaySource }
                },
                OutputOptions = new OutputOptions
                {
                    RecorderMode = RecorderMode.Video
                }
            };

            return options;
        }

        #endregion

        #region Region Recording

        private bool TryGetUsableSelectedArea(out Rectangle clampedDesktopRegion, out Screen targetScreen, out Rectangle localRegion)
        {
            clampedDesktopRegion = Rectangle.Empty;
            targetScreen = null;
            localRegion = Rectangle.Empty;

            if (!_selectedRecordingRegion.HasValue)
                return false;

            Rectangle selected = _selectedRecordingRegion.Value;
            if (selected.Width < MinRegionWidth || selected.Height < MinRegionHeight)
            {
                return false;
            }

            Screen[] screens = Screen.AllScreens;
            int maxArea = 0;
            Screen bestScreen = null;
            Rectangle bestIntersection = Rectangle.Empty;

            foreach (Screen screen in screens)
            {
                Rectangle intersection = Rectangle.Intersect(selected, screen.Bounds);
                int area = intersection.Width * intersection.Height;
                if (area > maxArea)
                {
                    maxArea = area;
                    bestScreen = screen;
                    bestIntersection = intersection;
                }
            }

            if (bestScreen == null || maxArea <= 0)
                return false;

            if (bestIntersection.Width < MinRegionWidth || bestIntersection.Height < MinRegionHeight)
                return false;

            int localX = bestIntersection.Left - bestScreen.Bounds.Left;
            int localY = bestIntersection.Top - bestScreen.Bounds.Top;
            int localW = bestIntersection.Width;
            int localH = bestIntersection.Height;

            if (localX < 0) localX = 0;
            if (localY < 0) localY = 0;
            if (localX + localW > bestScreen.Bounds.Width) localW = bestScreen.Bounds.Width - localX;
            if (localY + localH > bestScreen.Bounds.Height) localH = bestScreen.Bounds.Height - localY;

            if (localW < MinRegionWidth || localH < MinRegionHeight)
                return false;

            clampedDesktopRegion = bestIntersection;
            targetScreen = bestScreen;
            localRegion = new Rectangle(localX, localY, localW, localH);
            return true;
        }

        private IVideoEncoder GetVideoEncoder(string selectedCodec)
        {
            bool useQualityMode = vbrCheck.Checked;
            bool isHevc = selectedCodec == "H.265 (HEVC)";

            if (isHevc)
            {
                return new H265VideoEncoder
                {
                    BitrateMode = useQualityMode
                        ? H265BitrateControlMode.Quality
                        : H265BitrateControlMode.CBR,
                    EncoderProfile = H265Profile.Main
                };
            }

            return new H264VideoEncoder
            {
                BitrateMode = useQualityMode
                    ? H264BitrateControlMode.Quality
                    : H264BitrateControlMode.CBR,
                EncoderProfile = H264Profile.Main
            };
        }

        #endregion

        #region Output Validation

        private bool TryValidateOutputDirectory(string outputFilePath, out string errorMessage)
        {
            errorMessage = string.Empty;

            string directoryPath;
            try
            {
                directoryPath = Path.GetDirectoryName(outputFilePath);
            }
            catch (Exception ex)
            {
                errorMessage = "The output folder path is invalid.\n" + ex.Message;
                return false;
            }

            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                errorMessage = "The output folder path is invalid. Please select a valid recordings folder.";
                return false;
            }

            try
            {
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
            }
            catch (Exception ex)
            {
                errorMessage = "The output folder could not be created or accessed.\n" + ex.Message;
                return false;
            }

            string tempFilePath = Path.Combine(directoryPath, Guid.NewGuid().ToString("N") + ".tmp");
            try
            {
                using (FileStream fs = new FileStream(tempFilePath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    fs.WriteByte(0);
                }
                File.Delete(tempFilePath);
            }
            catch (Exception ex)
            {
                errorMessage = "Cannot write to the selected output folder.\nPlease choose a different folder.\n" + ex.Message;
                try
                {
                    if (File.Exists(tempFilePath))
                        File.Delete(tempFilePath);
                }
                catch
                {
                    // Ignore temp cleanup failures.
                }
                return false;
            }

            return true;
        }

        #endregion

        #region Recording Timer

        private void StopAndResetRecordingTimer()
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)StopAndResetRecordingTimer);
                return;
            }

            if (recordingTimer != null)
                recordingTimer.Stop();

            if (lblRecordingTimer != null)
                lblRecordingTimer.Text = "00:00:00";
        }

        private void RecordingTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - _recordingStartTime;
            lblRecordingTimer.Text = elapsed.ToString(@"hh\:mm\:ss");
        }

        #endregion
    }
}
