using ScreenRecorderLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SimpleScreenRecorder
{
    public partial class ScreenRecorder : Form
    {
        #region WinAPI Constants and Imports

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private const int WM_HOTKEY = 0x0312;
        private const int HOTKEY_ID_START = 9000;
        private const int HOTKEY_ID_STOP = 9001;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion

        #region Fields

        private Recorder _recorder;
        private string _outputPath;
        private readonly string _videosFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        private Dictionary<string, string> _inputDevicesMap;
        private List<RecordableDisplay> _availableDisplays;


        #endregion

        #region Constructor

        public ScreenRecorder()
        {
            InitializeComponent();

            appverLbl.Text = "v" + Application.ProductVersion;
            notifyIcon.ContextMenuStrip = contextMenuStrip;

            try
            {
                LoadAudioDevices();
                DetectMonitors();
                LoadSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading settings or devices: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (string.IsNullOrWhiteSpace(txtPath.Text))
                txtPath.Text = _videosFolder;

            InitializeToolTips();
        }

        #endregion

        #region Hotkey Management

        private const uint MOD_NOREPEAT = 0x4000;
        private Dictionary<int, bool> _hotkeyCooldown = new Dictionary<int, bool>();

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            RegisterHotkeys();
        }

        private void RegisterHotkeys()
        {
            bool startRegistered = RegisterHotKey(this.Handle, HOTKEY_ID_START, MOD_NOREPEAT, (uint)Keys.F9);
            bool stopRegistered = RegisterHotKey(this.Handle, HOTKEY_ID_STOP, MOD_NOREPEAT, (uint)Keys.F10);

            if (!startRegistered) Debug.WriteLine("Failed to register F9 hotkey.");
            if (!stopRegistered) Debug.WriteLine("Failed to register F10 hotkey.");

            _hotkeyCooldown[HOTKEY_ID_START] = false;
            _hotkeyCooldown[HOTKEY_ID_STOP] = false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();

                if (_hotkeyCooldown.ContainsKey(id) && _hotkeyCooldown[id])
                    return;

                _hotkeyCooldown[id] = true;

                var timer = new Timer { Interval = 500, Enabled = true };
                timer.Tick += (s, ev) =>
                {
                    _hotkeyCooldown[id] = false;
                    timer.Stop();
                    timer.Dispose();
                };

                _ = HandleHotkeyAsync(id);
                return;
            }

            base.WndProc(ref m);
        }

        private async Task HandleHotkeyAsync(int id)
        {
            try
            {
                if (id == HOTKEY_ID_START)
                {
                    if (_recorder == null || _recorder.Status != RecorderStatus.Recording)
                        await StartRecordingAsync(this, EventArgs.Empty);
                }
                else if (id == HOTKEY_ID_STOP)
                {
                    if (_recorder != null && _recorder.Status == RecorderStatus.Recording)
                        await StopRecording(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Hotkey handling error: " + ex.Message);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID_START);
            UnregisterHotKey(this.Handle, HOTKEY_ID_STOP);
            base.OnFormClosing(e);
        }

        #endregion

        #region Initialization


        private void InitializeToolTips()
        {
            toolTip.SetToolTip(btnBrowse, "Select the video output file location.");
            toolTip.SetToolTip(cbRecordSystemAudio, "Record all system (Windows) audio.");
            toolTip.SetToolTip(fpsLbl, "Adjust the recording framerate (FPS). Up to 120 FPS.");
            toolTip.SetToolTip(dspLbl, "Select the screen or monitor to record.");
            toolTip.SetToolTip(btnStart, "Start screen recording.");
            toolTip.SetToolTip(btnStop, "Stop the current recording.");
            toolTip.SetToolTip(hideonrecordChkBox, "Minimize the app to the system tray automatically when recording starts.");
            toolTip.SetToolTip(trackBarQuality, "Adjust the video quality and bitrate of the recording.");
            toolTip.SetToolTip(comboBoxMic, "Select a microphone for audio input.");
            toolTip.SetToolTip(comboBoxCodec, "Choose the video codec: H.264 (Best Compatibility) or H.265 (Best Efficiency).");
            toolTip.SetToolTip(cbrCheck, "Use Constant Bitrate (CBR) encoding (Results in larger file sizes).");
            toolTip.SetToolTip(vbrCheck, "Use Variable Bitrate (VBR) encoding based on quality (Results in smaller file sizes).");
        }

        private void LoadAudioDevices()
        {
            try
            {
                comboBoxMic.Items.Clear();
                comboBoxMic.Items.Add("None");

                var devices = Recorder.GetSystemAudioDevices(AudioDeviceSource.InputDevices);

                if (devices != null && devices.Count > 0)
                {
                    _inputDevicesMap = devices.ToDictionary(d => d.FriendlyName, d => d.DeviceName);
                    foreach (var device in devices)
                        comboBoxMic.Items.Add(device.FriendlyName);
                }
                else
                {
                    _inputDevicesMap = new Dictionary<string, string>();
                }
            }
            catch (Exception ex)
            {
                comboBoxMic.Items.Clear();
                comboBoxMic.Items.Add("None");
                _inputDevicesMap = new Dictionary<string, string>();
                MessageBox.Show("Error loading audio devices: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSettings()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.OutputPath))
                txtPath.Text = Properties.Settings.Default.OutputPath;

            hideonrecordChkBox.Checked = Properties.Settings.Default.HideOnRec;
            trackBarQuality.Value = Properties.Settings.Default.VideoQuality;
            fpsSelect.Value = Properties.Settings.Default.FrameRate;
            TrackBarQuality_Scroll(null, null);
            
            if (Properties.Settings.Default.VBREnabled)
            {
                vbrCheck.Checked = true;
                cbrCheck.Checked = false;
            }
            else
            {
                vbrCheck.Checked = false;
                cbrCheck.Checked = true;
            }

            cbRecordSystemAudio.Checked = Properties.Settings.Default.SystemAudioEnabled;

            int savedIndex = Properties.Settings.Default.MicSelectionIndex;
            if (savedIndex >= 0 && savedIndex < comboBoxMic.Items.Count)
                comboBoxMic.SelectedIndex = savedIndex;
            else
                comboBoxMic.SelectedIndex = 0;

            int savedDisplayIndex = Properties.Settings.Default.DisplaySelection;
            if (savedDisplayIndex >= 0 && savedDisplayIndex < comboBoxMonitor.Items.Count)
                comboBoxMonitor.SelectedIndex = savedDisplayIndex;
            else if (comboBoxMonitor.Items.Count > 0)
                comboBoxMonitor.SelectedIndex = 0;

            string savedCodec = Properties.Settings.Default.CodecSelection;
            if (comboBoxCodec.Items.Contains(savedCodec))
                comboBoxCodec.SelectedItem = savedCodec;
            else
                comboBoxCodec.SelectedIndex = 0;
        }

        #endregion

        #region Utility Methods

        private string GenerateFileName()
        {
            return "Recording_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".mp4";
        }

        private string ResolveOutputPath(string currentPathText)
        {
            string directory = string.IsNullOrWhiteSpace(currentPathText)
                ? _videosFolder
                : (Path.HasExtension(currentPathText)
                    ? Path.GetDirectoryName(currentPathText)
                    : currentPathText);

            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
                directory = _videosFolder;

            return Path.Combine(directory, GenerateFileName());
        }

        private int GetBitrateByQuality(int qualityLevel)
        {
            switch (qualityLevel)
            {
                case 1: return 3_000_000;
                case 2: return 10_000_000;
                case 3: return 25_000_000;
                case 4: return 50_000_000;
                case 5: return 100_000_000;
                default: return 10_000_000;
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            btnStart.Enabled = enabled;
            btnStop.Enabled = !enabled;
            btnBrowse.Enabled = enabled;
            comboBoxMic.Enabled = enabled;
            comboBoxMonitor.Enabled = enabled;
            fpsSelect.Enabled = enabled;
            trackBarQuality.Enabled = enabled;
            cbRecordSystemAudio.Enabled = enabled;
            lblStatus.ForeColor = enabled ? System.Drawing.Color.White : System.Drawing.Color.Red;

            if (startRecordingToolStripMenuItem != null)
                startRecordingToolStripMenuItem.Enabled = enabled;

            if (stopRecordingToolStripMenuItem != null)
                stopRecordingToolStripMenuItem.Enabled = !enabled;
        }

        private async Task StopAndDisposeRecorderAsync()
        {
            if (_recorder == null) return;

            try
            {
                if (_recorder.Status == RecorderStatus.Recording)
                {
                    var tcs = new System.Threading.Tasks.TaskCompletionSource<bool>();

                    void onComplete(object s, RecordingCompleteEventArgs e)
                    {
                        if (_recorder != null)
                        {
                            _recorder.OnRecordingComplete -= onComplete;
                        }
                        tcs.TrySetResult(true);
                    }

                    _recorder.OnRecordingComplete += onComplete;
                    _recorder.Stop();

                    await tcs.Task;
                }

                _recorder.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error stopping/disposing recorder: " + ex.Message);
                _recorder?.Dispose();
                _recorder = null;
            }
        }


        private void DetectMonitors()
        {
            try
            {
                _availableDisplays = Recorder.GetDisplays().ToList();
                comboBoxMonitor.Items.Clear();

                if (_availableDisplays == null || _availableDisplays.Count == 0)
                {
                    MessageBox.Show("No valid recording sources (monitors) were found.",
                        "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBoxMonitor.Enabled = false;
                    btnStart.Enabled = false;
                    return;
                }

                foreach (var display in _availableDisplays.Select((d, i) => new { Display = d, Index = i + 1 }))
                {
                    string uniqueName = $"{display.Index}. {display.Display.FriendlyName}";
                    comboBoxMonitor.Items.Add(uniqueName);
                }

                comboBoxMonitor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error detecting monitors: " + ex.Message,
                    "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxMonitor.Enabled = false;
                btnStart.Enabled = false;
            }
        }

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

            DisplayRecordingSource displaySource = new DisplayRecordingSource
            {
                DeviceName = selectedDisplay.DeviceName
            };

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

        private IVideoEncoder GetVideoEncoder(string selectedCodec)
        {
            bool useQualityMode = vbrCheck.Checked;
            bool isHevc = (selectedCodec == "H.265 (HEVC)");

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
            else
            {
                return new H264VideoEncoder
                {
                    BitrateMode = useQualityMode
                        ? H264BitrateControlMode.Quality
                        : H264BitrateControlMode.CBR,
                    EncoderProfile = H264Profile.Main
                };
            }
        }

        private void SaveSettings()
        {
            string path = txtPath.Text;

            string directoryToSave = Path.HasExtension(path)
                ? Path.GetDirectoryName(path)
                : path;

            Properties.Settings.Default.OutputPath = string.IsNullOrWhiteSpace(directoryToSave)
                ? _videosFolder
                : directoryToSave;

            Properties.Settings.Default.VideoQuality = trackBarQuality.Value;
            Properties.Settings.Default.FrameRate = (int)fpsSelect.Value;
            Properties.Settings.Default.SystemAudioEnabled = cbRecordSystemAudio.Checked;
            Properties.Settings.Default.MicSelectionIndex = comboBoxMic.SelectedIndex;
            Properties.Settings.Default.DisplaySelection = comboBoxMonitor.SelectedIndex;
            Properties.Settings.Default.CodecSelection = Convert.ToString(comboBoxCodec.SelectedItem);
            Properties.Settings.Default.VBREnabled = vbrCheck.Checked;
            Properties.Settings.Default.HideOnRec = hideonrecordChkBox.Checked;

            Properties.Settings.Default.Save();
        }

        private void ShowPath()
        {
            string savepath = txtPath.Text;

            if (Directory.Exists(savepath))
            {
                Process.Start("explorer.exe", savepath);
            }
            else
            {
                MessageBox.Show("The specified recordings path does not exist.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Recording Controls

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select folder to save recordings";
                fbd.SelectedPath = string.IsNullOrWhiteSpace(txtPath.Text) ? _videosFolder : txtPath.Text;
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = fbd.SelectedPath;
                    _outputPath = null;
                }
            }
        }

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            await StartRecordingAsync(sender, e);
        }

        private async Task StartRecordingAsync(object sender, EventArgs e)
        {
            if (_recorder != null && _recorder.Status == RecorderStatus.Recording)
            {
                MessageBox.Show("Recording is already in progress.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _outputPath = ResolveOutputPath(txtPath.Text);
                lblStatus.Text = "Status: Preparing...";
                SetControlsEnabled(false);

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
                    Framerate = (int)fpsSelect.Value,
                    Bitrate = GetBitrateByQuality(trackBarQuality.Value),
                    IsFixedFramerate = true,
                    IsHardwareEncodingEnabled = true,
                    Encoder = GetVideoEncoder(selectedCodec)
                };

                await Task.Run(() =>
                {
                    _recorder = Recorder.CreateRecorder(options);

                    _recorder.OnRecordingFailed += (s, evt) =>
                    {
                        if (IsHandleCreated && !IsDisposed)
                            this.BeginInvoke((MethodInvoker)(() =>
                            {
                                MessageBox.Show("Recording failed: " + evt.Error, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                lblStatus.Text = "Status: Error";
                                SetControlsEnabled(true);
                            }));
                    };

                    _recorder.OnRecordingComplete += (s, evt) =>
                    {
                        if (IsHandleCreated && !IsDisposed)
                            this.BeginInvoke((MethodInvoker)(() =>
                            {
                                try
                                {
                                    lblStatus.Text = "Status: Saved";
                                    SetControlsEnabled(true);
                                    txtPath.Text = Path.GetDirectoryName(evt.FilePath) ?? _videosFolder;
                                    _outputPath = null;

                                    if (notifyIcon.Visible)
                                    {
                                        notifyIcon.BalloonTipTitle = "Recording Complete";
                                        notifyIcon.BalloonTipText = "Recording saved to:\n" + evt.FilePath;
                                        notifyIcon.ShowBalloonTip(4000);
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
                    };

                    _recorder.OnStatusChanged += (s, evt) =>
                    {
                        if (IsHandleCreated && !IsDisposed)
                            this.BeginInvoke((MethodInvoker)(() =>
                            {
                                lblStatus.Text = "Status: " + ((RecorderStatus)evt.Status).ToString();

                                if (evt.Status == RecorderStatus.Recording && hideonrecordChkBox.Checked)
                                {
                                    WindowState = FormWindowState.Minimized;
                                    MinimizeApp();
                                }
                            }));
                    };

                    _recorder.Record(_outputPath);
                });

                lblStatus.Text = "Status: Recording...";
            }
            catch (Exception ex)
            {
                if (_recorder != null)
                {
                    _recorder.Dispose();
                    _recorder = null;
                }

                MessageBox.Show("Error starting recording:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetControlsEnabled(true);
            }
        }

        private async Task StopRecording(object sender, EventArgs e)
        {
            try
            {
                if (_recorder == null || _recorder.Status != RecorderStatus.Recording)
                {
                    lblStatus.Text = "Status: Idle";
                    return;
                }

                await StopAndDisposeRecorderAsync();
                lblStatus.Text = "Status: Stopped";
                SetControlsEnabled(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error stopping recording:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (_recorder != null)
                {
                    _recorder.Dispose();
                    _recorder = null;
                }
                SetControlsEnabled(true);
            }
        }

        private async void BtnStop_Click(object sender, EventArgs e)
        {
            await StopRecording(sender, e);
        }

        #endregion

        #region Form Events

        private void CbrCheck_CheckedChanged(object sender, EventArgs e)
        {
            vbrCheck.Checked = !cbrCheck.Checked;
        }

        private void VbrCheck_CheckedChanged(object sender, EventArgs e)
        {
            cbrCheck.Checked = !vbrCheck.Checked;
        }

        private async void ScreenRecorder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.None)
            {
                if (_recorder != null && _recorder.Status == RecorderStatus.Recording)
                {
                    DialogResult result = MessageBox.Show(
                        "Recording in progress. Stop and exit?",
                        "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }

                    e.Cancel = true;

                    await StopAndDisposeRecorderAsync();

                    SaveSettings();

                    this.BeginInvoke((MethodInvoker)delegate { this.Close(); });
                    return;
                }

                SaveSettings();
                e.Cancel = false;
            }
        }

        private void TrackBarQuality_Scroll(object sender, EventArgs e)
        {
            string qualityName;
            switch (trackBarQuality.Value)
            {
                case 1: qualityName = "Low (3 Mbps)"; break;
                case 2: qualityName = "Medium (10 Mbps)"; break;
                case 3: qualityName = "High (25 Mbps)"; break;
                case 4: qualityName = "Very High (50 Mbps)"; break;
                case 5: qualityName = "Maximum (100 Mbps)"; break;
                default: qualityName = "Medium (10 Mbps)"; break;
            }

            lblQualityValue.Text = "Quality: " + qualityName;
        }

        #endregion

        #region UI Controls and Window Management

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void HideBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            MinimizeApp();
        }

        private void MinimizeApp()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;

                if (_recorder != null && _recorder.Status == RecorderStatus.Recording)
                {
                    notifyIcon.BalloonTipTitle = "Info";
                    notifyIcon.BalloonTipText = "Recording is in progress...";
                }
                else
                {
                    notifyIcon.BalloonTipTitle = "Info";
                    notifyIcon.BalloonTipText = "Application minimized to tray.";
                }

                notifyIcon.ShowBalloonTip(3000);
            }
            else
            {
                notifyIcon.Visible = false;
            }
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void ShowCtxMenu_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void ExitCtxMenu_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private async void StartRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await StartRecordingAsync(sender, e);
        }

        private async void StopRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await StopRecording(sender, e);
        }

        private void ShowRecordingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPath();
        }

        private void ShowPath_Click(object sender, EventArgs e)
        {
            ShowPath();
        }

        private void Handle_Window_Drag(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #endregion

    }
}