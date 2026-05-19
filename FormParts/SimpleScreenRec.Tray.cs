using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    public partial class ScreenRecorder : Form
    {
        #region Tray State

        private string _lastCompletedRecordingFolder;
        private string _lastCompletedRecordingFile;
        private bool _isCompletionBalloonActive;

        #endregion

        #region Tray Events

        private void MinimizeApp()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
                _isCompletionBalloonActive = false;

                if (_recorder != null && _recorder.Status == ScreenRecorderLib.RecorderStatus.Recording)
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

        #endregion

        #region Completion Balloon

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            if (!_isCompletionBalloonActive)
                return;

            _isCompletionBalloonActive = false;
            string folder = _lastCompletedRecordingFolder;
            if (!string.IsNullOrWhiteSpace(folder) && Directory.Exists(folder))
            {
                Process.Start("explorer.exe", folder);
                return;
            }

            ShowPath();
        }

        #endregion

        #region Tray Events

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

        #endregion

        #region Open Folder / Open Last Recording

        private void OpenLastRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_lastCompletedRecordingFile) && File.Exists(_lastCompletedRecordingFile))
            {
                Process.Start(_lastCompletedRecordingFile);
                return;
            }

            MessageBox.Show("The last recorded file was not found. Please check the recordings folder.",
                "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
