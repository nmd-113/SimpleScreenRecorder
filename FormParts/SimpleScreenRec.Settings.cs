using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    public partial class ScreenRecorder : Form
    {
        #region Load Settings

        private void LoadSettings()
        {
            string savedPath = Properties.Settings.Default.OutputPath;
            string normalizedPath = string.IsNullOrWhiteSpace(savedPath)
                ? _videosFolder
                : (Path.HasExtension(savedPath) ? Path.GetDirectoryName(savedPath) : savedPath);
            if (string.IsNullOrWhiteSpace(normalizedPath) || !Directory.Exists(normalizedPath))
                normalizedPath = _videosFolder;
            txtPath.Text = normalizedPath;

            hideonrecordChkBox.Checked = Properties.Settings.Default.HideOnRec;
            countdownChkBox.Checked = Properties.Settings.Default.CountdownEnabled;
            int quality = Properties.Settings.Default.VideoQuality;
            quality = Math.Min(trackBarQuality.Maximum, Math.Max(trackBarQuality.Minimum, quality));
            trackBarQuality.Value = quality;

            comboBoxFps.SelectedIndex = GetFrameRateSelectionIndex(Properties.Settings.Default.FrameRate);
            TrackBarQuality_Scroll(null, null);

            bool useVbr = Properties.Settings.Default.VBREnabled;
            vbrCheck.Checked = useVbr;
            cbrCheck.Checked = !useVbr;
            if (vbrCheck.Checked == cbrCheck.Checked)
            {
                vbrCheck.Checked = true;
                cbrCheck.Checked = false;
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
            {
                comboBoxCodec.SelectedItem = savedCodec;
            }
            else if (comboBoxCodec.Items.Contains("H.264 (AVC)"))
            {
                comboBoxCodec.SelectedItem = "H.264 (AVC)";
            }
            else
            {
                comboBoxCodec.SelectedIndex = 0;
            }

            LoadSelectedAreaSettings();
        }

        #endregion

        #region Save Settings

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
            Properties.Settings.Default.FrameRate = GetSelectedFrameRate();
            Properties.Settings.Default.SystemAudioEnabled = cbRecordSystemAudio.Checked;
            Properties.Settings.Default.MicSelectionIndex = comboBoxMic.SelectedIndex;
            Properties.Settings.Default.DisplaySelection = comboBoxMonitor.SelectedIndex;
            Properties.Settings.Default.CodecSelection = Convert.ToString(comboBoxCodec.SelectedItem);
            Properties.Settings.Default.VBREnabled = vbrCheck.Checked;
            Properties.Settings.Default.HideOnRec = hideonrecordChkBox.Checked;
            Properties.Settings.Default.CountdownEnabled = countdownChkBox.Checked;
            SaveSelectedAreaSettings();

            Properties.Settings.Default.Save();
        }

        #endregion

        #region Settings Mapping Helpers

        private void LoadSelectedAreaSettings()
        {
            if (!Properties.Settings.Default.SelectedAreaEnabled)
            {
                _selectedRecordingRegion = null;
                UpdateAreaSelectionUi();
                return;
            }

            int width = Properties.Settings.Default.SelectedAreaWidth;
            int height = Properties.Settings.Default.SelectedAreaHeight;

            if (width <= 0 || height <= 0)
            {
                ClearInvalidSelectedAreaSettings();
                return;
            }

            Rectangle savedArea = new Rectangle(
                Properties.Settings.Default.SelectedAreaX,
                Properties.Settings.Default.SelectedAreaY,
                width,
                height);

            if (!TryNormalizeSavedSelectedArea(savedArea, out Rectangle normalizedArea))
            {
                ClearInvalidSelectedAreaSettings();
                return;
            }

            _selectedRecordingRegion = normalizedArea;
            UpdateAreaSelectionUi();
        }

        private void SaveSelectedAreaSettings()
        {
            if (_selectedRecordingRegion.HasValue)
            {
                Rectangle selectedArea = _selectedRecordingRegion.Value;
                Properties.Settings.Default.SelectedAreaEnabled = true;
                Properties.Settings.Default.SelectedAreaX = selectedArea.X;
                Properties.Settings.Default.SelectedAreaY = selectedArea.Y;
                Properties.Settings.Default.SelectedAreaWidth = selectedArea.Width;
                Properties.Settings.Default.SelectedAreaHeight = selectedArea.Height;
            }
            else
            {
                Properties.Settings.Default.SelectedAreaEnabled = false;
                Properties.Settings.Default.SelectedAreaX = 0;
                Properties.Settings.Default.SelectedAreaY = 0;
                Properties.Settings.Default.SelectedAreaWidth = 0;
                Properties.Settings.Default.SelectedAreaHeight = 0;
            }
        }

        private void ClearInvalidSelectedAreaSettings()
        {
            _selectedRecordingRegion = null;
            SaveSelectedAreaSettings();
            UpdateAreaSelectionUi();

            try
            {
                Properties.Settings.Default.Save();
            }
            catch
            {
                // Ignore startup persistence failures and fall back to the cleaned in-memory state.
            }
        }

        private bool TryNormalizeSavedSelectedArea(Rectangle savedArea, out Rectangle normalizedArea)
        {
            normalizedArea = Rectangle.Empty;

            if (savedArea.Width < MinRegionWidth || savedArea.Height < MinRegionHeight)
                return false;

            int maxArea = 0;
            Rectangle bestIntersection = Rectangle.Empty;

            foreach (Screen screen in Screen.AllScreens)
            {
                Rectangle intersection = Rectangle.Intersect(savedArea, screen.Bounds);
                int area = intersection.Width * intersection.Height;
                if (area > maxArea)
                {
                    maxArea = area;
                    bestIntersection = intersection;
                }
            }

            if (bestIntersection.Width < MinRegionWidth || bestIntersection.Height < MinRegionHeight)
                return false;

            normalizedArea = bestIntersection;
            return true;
        }

        #endregion
    }
}
