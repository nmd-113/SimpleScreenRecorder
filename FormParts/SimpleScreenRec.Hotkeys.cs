using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace SimpleScreenRecorder
{
    public partial class ScreenRecorder : Form
    {
        #region WinAPI

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private const int WM_HOTKEY = 0x0312;
        private const int HOTKEY_ID_START = 9000;
        private const int HOTKEY_ID_STOP = 9001;
        private const uint MOD_NOREPEAT = 0x4000;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion

        #region Fields

        private readonly Dictionary<int, bool> _hotkeyCooldown = new Dictionary<int, bool>();
        private bool _startHotkeyRegistered;
        private bool _stopHotkeyRegistered;
        private bool _hotkeyWarningShown;

        #endregion

        #region Registration

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (IsInDesignMode())
                return;

            RegisterHotkeys();
            UpdateSelectedAreaOverlay();
        }

        private void RegisterHotkeys()
        {
            _startHotkeyRegistered = RegisterHotKey(Handle, HOTKEY_ID_START, MOD_NOREPEAT, (uint)Keys.F9);
            _stopHotkeyRegistered = RegisterHotKey(Handle, HOTKEY_ID_STOP, MOD_NOREPEAT, (uint)Keys.F10);

            if (!_startHotkeyRegistered) Debug.WriteLine("Failed to register F9 hotkey.");
            if (!_stopHotkeyRegistered) Debug.WriteLine("Failed to register F10 hotkey.");

            _hotkeyCooldown[HOTKEY_ID_START] = false;
            _hotkeyCooldown[HOTKEY_ID_STOP] = false;

            if ((!_startHotkeyRegistered || !_stopHotkeyRegistered) && !_hotkeyWarningShown)
            {
                _hotkeyWarningShown = true;
                MessageBox.Show(
                    "Global hotkeys could not be fully registered.\n" +
                    "F9/F10 may be unavailable, but buttons and tray controls still work.",
                    "Hotkey Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Message Handling

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();

                if (id != HOTKEY_ID_START && id != HOTKEY_ID_STOP)
                {
                    base.WndProc(ref m);
                    return;
                }

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
                    await HandlePrimaryRecordingActionAsync(this, EventArgs.Empty);
                }
                else if (id == HOTKEY_ID_STOP)
                {
                    if (_recorder != null &&
                        (_recorder.Status == ScreenRecorderLib.RecorderStatus.Recording
                        || _recorder.Status == ScreenRecorderLib.RecorderStatus.Paused))
                        await StopRecording(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Hotkey handling error: " + ex.Message);
            }
        }

        #endregion

        #region Cleanup

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_startHotkeyRegistered)
                UnregisterHotKey(Handle, HOTKEY_ID_START);
            if (_stopHotkeyRegistered)
                UnregisterHotKey(Handle, HOTKEY_ID_STOP);
            CleanupTrayRecordingIndicator();
            base.OnFormClosing(e);
        }

        #endregion
    }
}
