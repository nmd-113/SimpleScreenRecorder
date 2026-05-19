using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    public partial class RegionSelectorForm : Form
    {
        #region Fields / Properties

        public Rectangle SelectedBounds { get; private set; }

        #endregion

        #region Constructor

        public RegionSelectorForm(Rectangle initialBounds)
        {
            InitializeComponent();

            KeyPreview = true;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            BackColor = Color.DeepSkyBlue;
            Opacity = 0.30;
            MinimumSize = new Size(320, 180);

            Rectangle bounds = initialBounds;
            if (bounds.Width <= 0 || bounds.Height <= 0)
                bounds = new Rectangle(100, 100, 1280, 720);

            Bounds = ClampToVirtualScreen(bounds);
            SelectedBounds = Bounds;
            UpdateSizeLabel();
        }

        #endregion

        #region UI Updates

        private void UpdateSizeLabel()
        {
            lblCurrentSize.Text = $"{Width} x {Height}";
            SelectedBounds = Bounds;
        }

        #endregion

        #region Confirm / Cancel

        private void ConfirmSelection()
        {
            SelectedBounds = Bounds;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RegionSelectorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                ConfirmSelection();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void RegionSelectorForm_Move(object sender, EventArgs e)
        {
            UpdateSizeLabel();
        }

        private void RegionSelectorForm_Resize(object sender, EventArgs e)
        {
            UpdateSizeLabel();
        }

        private void RegionSelectorForm_DoubleClick(object sender, EventArgs e)
        {
            ConfirmSelection();
        }

        #endregion

        #region Bounds Helpers

        private static Rectangle ClampToVirtualScreen(Rectangle rect)
        {
            Rectangle virtualScreen = SystemInformation.VirtualScreen;
            int width = Math.Min(rect.Width, virtualScreen.Width);
            int height = Math.Min(rect.Height, virtualScreen.Height);

            int x = rect.X;
            int y = rect.Y;

            if (x < virtualScreen.Left) x = virtualScreen.Left;
            if (y < virtualScreen.Top) y = virtualScreen.Top;
            if (x + width > virtualScreen.Right) x = virtualScreen.Right - width;
            if (y + height > virtualScreen.Bottom) y = virtualScreen.Bottom - height;

            return new Rectangle(x, y, width, height);
        }

        #endregion
    }
}
