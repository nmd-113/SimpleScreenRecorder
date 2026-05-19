using System;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    public partial class ScreenRecorder : Form
    {
        #region Constructor

        public ScreenRecorder()
        {
            InitializeComponent();

            appverLbl.Text = "by NaeTech  |  v" + Application.ProductVersion;
            notifyIcon.ContextMenuStrip = contextMenuStrip;
            notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;

            bool isDesignMode = IsInDesignMode();
            if (!isDesignMode)
            {
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
            }

            if (string.IsNullOrWhiteSpace(txtPath.Text))
                txtPath.Text = _videosFolder;

            InitializeToolTips();
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
            SaveSettings();

            if (_isClosingAfterRecordingStop)
            {
                e.Cancel = false;
                return;
            }

            bool isActiveRecording = _recorder != null && _recorder.Status == ScreenRecorderLib.RecorderStatus.Recording;
            if (isActiveRecording || _isStoppingRecording)
            {
                var result = MessageBox.Show(
                    "Recording in progress. Stop and exit?",
                    "Confirm Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                e.Cancel = true;

                try
                {
                    await StopAndDisposeRecorderAsync();
                    _isClosingAfterRecordingStop = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error stopping recorder: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }

            e.Cancel = false;
        }

        #endregion

        #region Basic UI Events

        private void TrackBarQuality_Scroll(object sender, EventArgs e)
        {
            string qualityName;
            switch (trackBarQuality.Value)
            {
                case 1: qualityName = "Very Low (2 Mbps)"; break;
                case 2: qualityName = "Low (4 Mbps)"; break;
                case 3: qualityName = "Basic (6 Mbps)"; break;
                case 4: qualityName = "Standard (8 Mbps)"; break;
                case 5: qualityName = "Good (12 Mbps)"; break;
                case 6: qualityName = "Better (16 Mbps)"; break;
                case 7: qualityName = "High (24 Mbps)"; break;
                case 8: qualityName = "Very High (35 Mbps)"; break;
                case 9: qualityName = "Ultra (50 Mbps)"; break;
                case 10: qualityName = "Maximum (80 Mbps)"; break;
                default: qualityName = "Better (16 Mbps)"; break;
            }

            lblQualityValue.Text = "Quality: " + qualityName;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveSettings();
                Close();
            }
        }

        private void HideBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            MinimizeApp();
        }

        #endregion
    }
}
