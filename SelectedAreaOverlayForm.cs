using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    internal sealed class SelectedAreaOverlayForm : Form
    {
        private const int BorderOffset = 5;
        private const int WmNcHitTest = 0x0084;
        private static readonly IntPtr HtTransparent = new IntPtr(-1);
        private Rectangle _selectedBounds;

        public SelectedAreaOverlayForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;
            BackColor = Color.Fuchsia;
            TransparencyKey = Color.Fuchsia;
            StartPosition = FormStartPosition.Manual;
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WsExTransparent = 0x20;
                const int WsExToolWindow = 0x80;
                const int WsExNoActivate = 0x08000000;

                CreateParams parameters = base.CreateParams;
                parameters.ExStyle |= WsExTransparent | WsExToolWindow | WsExNoActivate;
                return parameters;
            }
        }

        public void ShowFor(Rectangle selectedBounds)
        {
            _selectedBounds = selectedBounds;
            Bounds = SystemInformation.VirtualScreen;

            if (!Visible)
                Show();

            Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmNcHitTest)
            {
                m.Result = HtTransparent;
                return;
            }

            base.WndProc(ref m);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_selectedBounds.Width <= 0 || _selectedBounds.Height <= 0)
                return;

            Rectangle borderBounds = Rectangle.Inflate(_selectedBounds, BorderOffset, BorderOffset);
            borderBounds.Offset(-Bounds.Left, -Bounds.Top);

            e.Graphics.SmoothingMode = SmoothingMode.None;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            using (Pen borderPen = new Pen(Color.FromArgb(115, 145, 175, 180), 1f))
            {
                borderPen.DashStyle = DashStyle.Dot;
                e.Graphics.DrawRectangle(borderPen, borderBounds);
            }
        }
    }
}
