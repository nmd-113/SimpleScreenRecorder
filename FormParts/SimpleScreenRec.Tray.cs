using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    public partial class ScreenRecorder : Form
    {
        #region Tray State

        private string _lastCompletedRecordingFolder;
        private string _lastCompletedRecordingFile;
        private bool _isCompletionBalloonActive;
        private Timer _trayRecordingIndicatorTimer;
        private Icon _defaultTrayIcon;
        private Icon _recordingTrayIcon;
        private Icon _recordingTrayIconPulse;
        private Icon _pausedTrayIcon;
        private bool _isTrayRecordingIndicatorVisible;

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
                    StartTrayRecordingIndicator();
                    return;
                }

                if (_recorder != null && _recorder.Status == ScreenRecorderLib.RecorderStatus.Paused)
                {
                    ShowPausedTrayIndicator();
                    return;
                }

                notifyIcon.BalloonTipTitle = "Info";
                notifyIcon.BalloonTipText = "Application minimized to tray.";
                notifyIcon.ShowBalloonTip(3000);
            }
            else
            {
                if (_trayRecordingIndicatorTimer != null)
                    _trayRecordingIndicatorTimer.Stop();

                notifyIcon.Visible = false;
            }
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        #endregion

        #region Recording Indicator

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool DestroyIcon(IntPtr handle);

        private void InitializeTrayRecordingIndicator()
        {
            if (components == null || notifyIcon == null || notifyIcon.Icon == null)
                return;

            _defaultTrayIcon = (Icon)notifyIcon.Icon.Clone();
            _recordingTrayIcon = CreateRecordingTrayIcon(_defaultTrayIcon);
            _recordingTrayIconPulse = CreateRecordingTrayIcon(_defaultTrayIcon, Color.FromArgb(170, 90, 255, 140));
            _pausedTrayIcon = CreatePausedTrayIcon(_defaultTrayIcon);
            _trayRecordingIndicatorTimer = new Timer(components) { Interval = 500 };
            _trayRecordingIndicatorTimer.Tick += TrayRecordingIndicatorTimer_Tick;
        }

        private void StartTrayRecordingIndicator()
        {
            if (_trayRecordingIndicatorTimer == null || _recordingTrayIcon == null || _recordingTrayIconPulse == null)
                return;

            _isTrayRecordingIndicatorVisible = true;
            notifyIcon.Icon = _recordingTrayIcon;

            if (notifyIcon.Visible)
                _trayRecordingIndicatorTimer.Start();
            else
                _trayRecordingIndicatorTimer.Stop();
        }

        private void ShowPausedTrayIndicator()
        {
            if (_trayRecordingIndicatorTimer != null)
                _trayRecordingIndicatorTimer.Stop();

            _isTrayRecordingIndicatorVisible = false;

            if (notifyIcon != null && _pausedTrayIcon != null)
                notifyIcon.Icon = _pausedTrayIcon;
        }

        private void StopTrayRecordingIndicator()
        {
            if (_trayRecordingIndicatorTimer != null)
                _trayRecordingIndicatorTimer.Stop();

            _isTrayRecordingIndicatorVisible = false;

            if (notifyIcon != null && _defaultTrayIcon != null)
                notifyIcon.Icon = _defaultTrayIcon;
        }

        private void CleanupTrayRecordingIndicator()
        {
            if (_trayRecordingIndicatorTimer != null)
            {
                _trayRecordingIndicatorTimer.Stop();
                _trayRecordingIndicatorTimer.Tick -= TrayRecordingIndicatorTimer_Tick;
                _trayRecordingIndicatorTimer.Dispose();
                _trayRecordingIndicatorTimer = null;
            }

            if (_recordingTrayIcon != null)
            {
                _recordingTrayIcon.Dispose();
                _recordingTrayIcon = null;
            }

            if (_recordingTrayIconPulse != null)
            {
                _recordingTrayIconPulse.Dispose();
                _recordingTrayIconPulse = null;
            }

            if (_pausedTrayIcon != null)
            {
                _pausedTrayIcon.Dispose();
                _pausedTrayIcon = null;
            }

            if (_defaultTrayIcon != null)
            {
                _defaultTrayIcon.Dispose();
                _defaultTrayIcon = null;
            }
        }

        private void TrayRecordingIndicatorTimer_Tick(object sender, EventArgs e)
        {
            if (notifyIcon == null || _recordingTrayIcon == null || _recordingTrayIconPulse == null)
                return;

            _isTrayRecordingIndicatorVisible = !_isTrayRecordingIndicatorVisible;
            notifyIcon.Icon = _isTrayRecordingIndicatorVisible ? _recordingTrayIcon : _recordingTrayIconPulse;
        }

        private static Icon CreateRecordingTrayIcon(Icon baseIcon)
        {
            return CreateRecordingTrayIcon(baseIcon, Color.FromArgb(245, 90, 255, 140));
        }

        private static Icon CreateRecordingTrayIcon(Icon baseIcon, Color badgeColor)
        {
            using (Bitmap bitmap = CreateTraySizedBitmap(baseIcon))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                Rectangle badgeBounds = new Rectangle(bitmap.Width - 10, bitmap.Height - 10, 9, 9);
                Rectangle shadowBounds = new Rectangle(badgeBounds.X + 1, badgeBounds.Y + 1, badgeBounds.Width, badgeBounds.Height);
                using (Brush fillBrush = new SolidBrush(badgeColor))
                using (Brush shadowBrush = new SolidBrush(Color.FromArgb(90, 0, 0, 0)))
                using (Pen outerPen = new Pen(Color.FromArgb(240, 15, 15, 15), 1f))
                using (Pen innerPen = new Pen(Color.FromArgb(240, 255, 255, 255), 1f))
                {
                    graphics.FillEllipse(shadowBrush, shadowBounds);
                    graphics.FillEllipse(fillBrush, badgeBounds);
                    graphics.DrawEllipse(outerPen, badgeBounds);

                    Rectangle innerBounds = new Rectangle(
                        badgeBounds.X + 1,
                        badgeBounds.Y + 1,
                        badgeBounds.Width - 2,
                        badgeBounds.Height - 2);
                    graphics.DrawEllipse(innerPen, innerBounds);
                }

                IntPtr iconHandle = bitmap.GetHicon();
                try
                {
                    using (Icon generatedIcon = Icon.FromHandle(iconHandle))
                    {
                        return (Icon)generatedIcon.Clone();
                    }
                }
                finally
                {
                    DestroyIcon(iconHandle);
                }
            }
        }

        private static Icon CreatePausedTrayIcon(Icon baseIcon)
        {
            using (Bitmap bitmap = CreateTraySizedBitmap(baseIcon))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                Rectangle badgeBounds = new Rectangle(bitmap.Width - 10, bitmap.Height - 10, 9, 9);
                Rectangle shadowBounds = new Rectangle(badgeBounds.X + 1, badgeBounds.Y + 1, badgeBounds.Width, badgeBounds.Height);
                Rectangle leftBar = new Rectangle(badgeBounds.X + 2, badgeBounds.Y + 2, 2, 5);
                Rectangle rightBar = new Rectangle(badgeBounds.X + 5, badgeBounds.Y + 2, 2, 5);
                using (Brush fillBrush = new SolidBrush(Color.FromArgb(245, 255, 203, 79)))
                using (Brush shadowBrush = new SolidBrush(Color.FromArgb(90, 0, 0, 0)))
                using (Brush barBrush = new SolidBrush(Color.FromArgb(240, 40, 40, 40)))
                using (Pen outerPen = new Pen(Color.FromArgb(240, 15, 15, 15), 1f))
                using (Pen innerPen = new Pen(Color.FromArgb(240, 255, 255, 255), 1f))
                {
                    graphics.FillEllipse(shadowBrush, shadowBounds);
                    graphics.FillEllipse(fillBrush, badgeBounds);
                    graphics.DrawEllipse(outerPen, badgeBounds);

                    Rectangle innerBounds = new Rectangle(
                        badgeBounds.X + 1,
                        badgeBounds.Y + 1,
                        badgeBounds.Width - 2,
                        badgeBounds.Height - 2);
                    graphics.DrawEllipse(innerPen, innerBounds);
                    graphics.FillRectangle(barBrush, leftBar);
                    graphics.FillRectangle(barBrush, rightBar);
                }

                IntPtr iconHandle = bitmap.GetHicon();
                try
                {
                    using (Icon generatedIcon = Icon.FromHandle(iconHandle))
                    {
                        return (Icon)generatedIcon.Clone();
                    }
                }
                finally
                {
                    DestroyIcon(iconHandle);
                }
            }
        }

        private static Bitmap CreateTraySizedBitmap(Icon baseIcon)
        {
            Size trayIconSize = SystemInformation.SmallIconSize;
            Bitmap bitmap = new Bitmap(trayIconSize.Width, trayIconSize.Height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.DrawIcon(baseIcon, new Rectangle(Point.Empty, trayIconSize));
            }

            return bitmap;
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
            await HandlePrimaryRecordingActionAsync(sender, e);
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
