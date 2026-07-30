using ScreenRecorderLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    public partial class ScreenRecorder : Form
    {
        #region Fields

        private string _outputPath;
        private readonly string _videosFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        private Dictionary<string, string> _inputDevicesMap;
        private List<RecordableDisplay> _availableDisplays;
        private Rectangle? _selectedRecordingRegion;

        #endregion

        #region Initialization Helpers

        private void InitializeToolTips()
        {
            toolTip.SetToolTip(comboBoxMic, "Select a microphone for audio input, or leave it on None.");
            toolTip.SetToolTip(cbRecordSystemAudio, "Record system audio such as app sound, music, and browser audio.");

            toolTip.SetToolTip(comboBoxCodec, "Choose the video codec: H.264 for compatibility or H.265 for smaller files on supported systems.");
            toolTip.SetToolTip(comboBoxFps, "Higher FPS gives smoother video but larger files and higher GPU/CPU usage.");
            toolTip.SetToolTip(comboBoxMonitor, "Select the display that will be recorded when no custom area is active.");
            toolTip.SetToolTip(trackBarQuality, "Higher quality gives sharper video but larger files.");
            toolTip.SetToolTip(vbrCheck, "Variable Bitrate adjusts bitrate as needed for smaller files.");
            toolTip.SetToolTip(cbrCheck, "Constant Bitrate keeps bitrate steadier for more predictable file output.");
            toolTip.SetToolTip(btnSelectArea, "Open the area selector and choose a custom part of the screen to record.");

            toolTip.SetToolTip(btnBrowse, "Choose a folder for new recordings.");
            toolTip.SetToolTip(showPath, "Open the current recordings folder.");

            toolTip.SetToolTip(countdownChkBox, "Wait 3 seconds before recording starts.");
            toolTip.SetToolTip(hideonrecordChkBox, "Automatically minimize the app to the tray after recording starts.");
        }

        #endregion

        #region Device / Monitor Loading

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

        #endregion

        #region Path Helpers

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

        #endregion

        #region Quality / FPS Helpers

        private int GetBitrateByQuality(int qualityLevel)
        {
            switch (qualityLevel)
            {
                case 1: return 2_000_000;
                case 2: return 4_000_000;
                case 3: return 6_000_000;
                case 4: return 8_000_000;
                case 5: return 12_000_000;
                case 6: return 16_000_000;
                case 7: return 24_000_000;
                case 8: return 35_000_000;
                case 9: return 50_000_000;
                case 10: return 80_000_000;
                default: return 16_000_000;
            }
        }

        private int GetSelectedFrameRate()
        {
            return comboBoxFps.SelectedIndex == 2 ? 120 : (comboBoxFps.SelectedIndex == 1 ? 60 : 30);
        }

        private int GetFrameRateSelectionIndex(int frameRate)
        {
            if (frameRate >= 120)
                return 2;
            if (frameRate >= 60)
                return 1;
            return 0;
        }

        #endregion

        #region Area Helpers

        private void UpdateAreaSelectionUi()
        {
            btnSelectArea.Text = "Select Area";
            btnClearArea.Enabled = _selectedRecordingRegion.HasValue;

            if (_selectedRecordingRegion.HasValue)
            {
                areaStatusLabel.Text = $"Area: {_selectedRecordingRegion.Value.Width}x{_selectedRecordingRegion.Value.Height}";
            }
            else
            {
                areaStatusLabel.Text = "Area: Full Display";
            }
        }

        #endregion

        #region UI Helpers

        private void SetControlsEnabled(bool enabled)
        {
            bool canUsePrimaryAction = enabled
                || (_recorder != null
                    && (_recorder.Status == RecorderStatus.Recording || _recorder.Status == RecorderStatus.Paused)
                    && !_isStoppingRecording
                    && !_isCountdownActive);

            btnStart.Enabled = canUsePrimaryAction;
            btnStop.Enabled = !enabled;
            btnSelectArea.Enabled = enabled;
            btnClearArea.Enabled = enabled && _selectedRecordingRegion.HasValue;
            btnBrowse.Enabled = enabled;
            comboBoxMic.Enabled = enabled;
            comboBoxMonitor.Enabled = enabled;
            comboBoxCodec.Enabled = enabled;
            comboBoxFps.Enabled = enabled;
            trackBarQuality.Enabled = enabled;
            cbrCheck.Enabled = enabled;
            vbrCheck.Enabled = enabled;
            countdownChkBox.Enabled = enabled;
            cbRecordSystemAudio.Enabled = enabled;
            lblStatus.ForeColor = enabled ? Color.White : Color.Red;

            if (startRecordingToolStripMenuItem != null)
                startRecordingToolStripMenuItem.Enabled = canUsePrimaryAction;

            if (stopRecordingToolStripMenuItem != null)
                stopRecordingToolStripMenuItem.Enabled = !enabled;

            UpdatePrimaryActionUi();
        }

        private void UpdatePrimaryActionUi()
        {
            string buttonText = "⚫ Start Recording (F9)";
            string menuText = "⚫ Start Recording";
            string tooltipText = "Start recording with the current settings.";

            if (_recorder != null && _recorder.Status == RecorderStatus.Paused)
            {
                buttonText = "▶︎ Resume Recording (F9)";
                menuText = "▶︎ Resume Recording";
                tooltipText = "Resume the paused recording.";
            }
            else if (_recorder != null && _recorder.Status == RecorderStatus.Recording)
            {
                buttonText = "❚❚ Pause Recording (F9)";
                menuText = "❚❚ Pause Recording";
                tooltipText = "Pause the current recording.";
            }

            btnStart.Text = buttonText;

            if (startRecordingToolStripMenuItem != null)
                startRecordingToolStripMenuItem.Text = menuText;

            if (toolTip != null)
                toolTip.SetToolTip(btnStart, tooltipText);
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

        public void FlashLabel(Label lbl, bool enable, int interval = 500)
        {
            if (lbl == null || lbl.IsDisposed)
                return;

            CancellationTokenSource previousCts = lbl.Tag as CancellationTokenSource;

            if (enable)
            {
                if (previousCts != null)
                {
                    previousCts.Cancel();
                    previousCts.Dispose();
                }

                var cts = new CancellationTokenSource();
                lbl.Tag = cts;

                _ = Task.Run(async () =>
                {
                    bool on = false;
                    try
                    {
                        while (!cts.Token.IsCancellationRequested)
                        {
                            if (lbl.IsDisposed || !lbl.IsHandleCreated)
                                break;

                            if (lbl.InvokeRequired)
                            {
                                lbl.BeginInvoke((Action)(() =>
                                {
                                    if (!lbl.IsDisposed && ReferenceEquals(lbl.Tag, cts))
                                        lbl.ForeColor = on ? Color.Red : Color.White;
                                }));
                            }
                            else
                            {
                                if (ReferenceEquals(lbl.Tag, cts))
                                    lbl.ForeColor = on ? Color.Red : Color.White;
                            }

                            on = !on;
                            await Task.Delay(interval, cts.Token);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                    finally
                    {
                        try
                        {
                            if (!lbl.IsDisposed && lbl.IsHandleCreated)
                            {
                                if (lbl.InvokeRequired)
                                {
                                    lbl.BeginInvoke((Action)(() =>
                                    {
                                        if (!lbl.IsDisposed && ReferenceEquals(lbl.Tag, cts))
                                            lbl.ForeColor = Color.White;
                                    }));
                                }
                                else
                                {
                                    if (ReferenceEquals(lbl.Tag, cts))
                                        lbl.ForeColor = Color.White;
                                }
                            }
                        }
                        catch (ObjectDisposedException)
                        {
                        }
                    }
                });
            }
            else if (previousCts != null)
            {
                previousCts.Cancel();
                previousCts.Dispose();

                if (!lbl.IsDisposed)
                    lbl.ForeColor = Color.White;

                lbl.Tag = null;
            }
        }

        #endregion

        #region Basic UI Events

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

        #endregion

        #region Area Selection

        private void BtnSelectArea_Click(object sender, EventArgs e)
        {
            Rectangle initialBounds = _selectedRecordingRegion ?? GetDefaultSelectionBounds();

            using (var selector = new RegionSelectorForm(initialBounds))
            {
                if (selector.ShowDialog(this) == DialogResult.OK)
                {
                    _selectedRecordingRegion = selector.SelectedBounds;
                    UpdateAreaSelectionUi();
                    lblStatus.Text = $"Status: Area selected {_selectedRecordingRegion.Value.Width}x{_selectedRecordingRegion.Value.Height}";
                }
            }
        }

        private void BtnClearArea_Click(object sender, EventArgs e)
        {
            _selectedRecordingRegion = null;
            UpdateAreaSelectionUi();
            lblStatus.Text = "Status: Full display mode.";
        }

        private Rectangle GetDefaultSelectionBounds()
        {
            const int defaultWidth = 1280;
            const int defaultHeight = 720;

            Screen targetScreen = Screen.PrimaryScreen;
            if (comboBoxMonitor.SelectedIndex >= 0 && comboBoxMonitor.SelectedIndex < Screen.AllScreens.Length)
            {
                targetScreen = Screen.AllScreens[comboBoxMonitor.SelectedIndex];
            }

            Rectangle area = targetScreen.WorkingArea;
            int width = Math.Min(defaultWidth, area.Width);
            int height = Math.Min(defaultHeight, area.Height);
            int x = area.Left + (area.Width - width) / 2;
            int y = area.Top + (area.Height - height) / 2;

            Rectangle centered = new Rectangle(x, y, width, height);
            Rectangle virtualScreen = SystemInformation.VirtualScreen;
            centered.Intersect(virtualScreen);
            if (centered.Width <= 0 || centered.Height <= 0)
            {
                centered = new Rectangle(virtualScreen.Left, virtualScreen.Top, Math.Min(defaultWidth, virtualScreen.Width), Math.Min(defaultHeight, virtualScreen.Height));
            }
            return centered;
        }

        #endregion

        #region Design-Time Helpers

        private bool IsInDesignMode()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;

            if (DesignMode)
                return true;

            string processName = Process.GetCurrentProcess().ProcessName;
            return processName.IndexOf("devenv", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion

        #region Window Chrome

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
