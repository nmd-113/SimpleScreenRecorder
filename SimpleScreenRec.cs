using ScreenRecorderLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    public partial class ScreenRecorder : Form
    {
        #region WinAPI Constants and Imports

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        #endregion

        #region Fields

        private Recorder _recorder;
        private string _outputPath;
        private readonly string _videosFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        private Dictionary<string, string> _inputDevicesMap;

        #endregion

        #region Constructor

        public ScreenRecorder()
        {
            InitializeComponent();
            appverLbl.Text = "v" + Application.ProductVersion;

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

        #region Initialization

        private void InitializeToolTips()
        {
            toolTip.SetToolTip(btnBrowse, "Browse to select output file location.");
            toolTip.SetToolTip(cbRecordSystemAudio, "Record Windows system audio.");
            toolTip.SetToolTip(fpsLbl, "Increase the framerate of the recording up to 120FPS.");
            toolTip.SetToolTip(dspLbl, "Select the screen you want to record.");
            toolTip.SetToolTip(btnStart, "Start recording the screen.");
            toolTip.SetToolTip(btnStop, "Stop the current recording.");
            toolTip.SetToolTip(hideonrecordChkBox, "Automatically minimize the app to the system tray when recording starts.");
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
                MessageBox.Show("Error loading audio devices: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSettings()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.OutputPath))
                txtPath.Text = Properties.Settings.Default.OutputPath;

            trackBarQuality.Value = Properties.Settings.Default.VideoQuality;
            fpsSelect.Value = Properties.Settings.Default.FrameRate;
            TrackBarQuality_Scroll(null, null);

            cbRecordSystemAudio.Checked = Properties.Settings.Default.SystemAudioEnabled;

            int savedIndex = Properties.Settings.Default.MicSelectionIndex;
            if (savedIndex >= 0 && savedIndex < comboBoxMic.Items.Count)
                comboBoxMic.SelectedIndex = savedIndex;
            else
                comboBoxMic.SelectedIndex = 0;

            int savedDisplay = Properties.Settings.Default.DisplaySelection;
            if (savedDisplay >= numericUpDownMonitor.Minimum && savedDisplay <= numericUpDownMonitor.Maximum)
                numericUpDownMonitor.Value = savedDisplay;
            else
                numericUpDownMonitor.Value = 1;
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
            fpsSelect.Enabled = enabled;
            trackBarQuality.Enabled = enabled;
            cbRecordSystemAudio.Enabled = enabled;
            lblStatus.ForeColor = enabled ? System.Drawing.Color.White : System.Drawing.Color.Red;
        }

        private void DisposeRecorder()
        {
            if (_recorder != null)
            {
                try
                {
                    if (_recorder.Status == RecorderStatus.Recording)
                        _recorder.Stop();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error stopping recorder: " + ex.Message);
                }
                finally
                {
                    _recorder.Dispose();
                    _recorder = null;
                }
            }
        }

        private void DetectMonitors()
        {
            int monitorCount = Screen.AllScreens.Length;
            numericUpDownMonitor.Minimum = 1;
            numericUpDownMonitor.Maximum = Math.Max(1, monitorCount);
            numericUpDownMonitor.Value = 1;
        }

        private RecorderOptions GetSelectedMonitorOptions()
        {
            int selectedMonitorIndex = (int)numericUpDownMonitor.Value - 1;
            var screens = Screen.AllScreens;

            if (selectedMonitorIndex < 0 || selectedMonitorIndex >= screens.Length)
                throw new ArgumentOutOfRangeException(nameof(selectedMonitorIndex), "Monitor selection is out of range.");

            string deviceName = $@"\\.\DISPLAY{selectedMonitorIndex + 1}";

            var displaySource = new DisplayRecordingSource
            {
                DeviceName = deviceName
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

        private void SaveSettings()
        {
            string path = txtPath.Text;
            Properties.Settings.Default.OutputPath = Path.HasExtension(path)
                ? Path.GetDirectoryName(path)
                : path;

            Properties.Settings.Default.VideoQuality = trackBarQuality.Value;
            Properties.Settings.Default.FrameRate = (int)fpsSelect.Value;
            Properties.Settings.Default.SystemAudioEnabled = cbRecordSystemAudio.Checked;
            Properties.Settings.Default.MicSelectionIndex = comboBoxMic.SelectedIndex;
            Properties.Settings.Default.DisplaySelection = (int)numericUpDownMonitor.Value;

            Properties.Settings.Default.Save();
        }

        #endregion

        #region Recording Controls

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Select Save Location";
                sfd.Filter = "MP4 Video (*.mp4)|*.mp4";
                sfd.FileName = GenerateFileName();

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _outputPath = sfd.FileName;
                    txtPath.Text = _outputPath;
                }
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            try
            {
                _outputPath = ResolveOutputPath(txtPath.Text);
                txtPath.Text = _outputPath;

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

                options.VideoEncoderOptions = new VideoEncoderOptions
                {
                    Framerate = (int)fpsSelect.Value,
                    Bitrate = GetBitrateByQuality(trackBarQuality.Value),
                    IsFixedFramerate = true,
                    IsHardwareEncodingEnabled = true
                };

                _recorder = Recorder.CreateRecorder(options);

                _recorder.OnRecordingFailed += (s, evt) =>
                {
                    if (IsHandleCreated && !Disposing)
                        this.Invoke((MethodInvoker)delegate
                        {
                            MessageBox.Show("Recording failed: " + evt.Error, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            lblStatus.Text = "Status: Error";
                            SetControlsEnabled(true);
                        });
                };

                _recorder.OnRecordingComplete += (s, evt) =>
                {
                    if (IsHandleCreated && !Disposing)
                        this.Invoke((MethodInvoker)delegate
                        {
                            try
                            {
                                lblStatus.Text = "Status: Saved";
                                SetControlsEnabled(true);
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
                        });
                };

                _recorder.OnStatusChanged += (s, evt) =>
                {
                    if (IsHandleCreated && !Disposing)
                        this.Invoke((MethodInvoker)delegate
                        {
                            lblStatus.Text = "Status: " + ((RecorderStatus)evt.Status).ToString();

                            if (hideonrecordChkBox.Checked)
                            {
                                WindowState = FormWindowState.Minimized;
                                MinimizeApp();
                            }
                        });
                };

                _recorder.Record(_outputPath);
                SetControlsEnabled(false);
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

        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_recorder == null || _recorder.Status != RecorderStatus.Recording)
                {
                    lblStatus.Text = "Status: Idle";
                    return;
                }

                DisposeRecorder();
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

        #endregion

        #region Form Events

        private void ScreenRecorder_FormClosing(object sender, FormClosingEventArgs e)
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

                DisposeRecorder();
            }

            SaveSettings();
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

            lblQualityValue.Text = "Video Quality: " + qualityName;
        }

        #endregion

        #region UI Controls and Window Management

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
                ShowInTaskbar = false;

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
                ShowInTaskbar = true;
            }
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }

        private void ScreenRecorder_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { ReleaseCapture(); SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); }
        }

        private void Label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { ReleaseCapture(); SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); }
        }

        private void AppverLbl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { ReleaseCapture(); SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { ReleaseCapture(); SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); }
        }

        private void LblStatus_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { ReleaseCapture(); SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); }
        }

        #endregion
    }
}