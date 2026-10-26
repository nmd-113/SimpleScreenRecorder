using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    public partial class ScreenRecorder
    {
        private void ApplyUiAppearance()
        {
            EmbeddedFont.Apply(this);

            Font menuFont = EmbeddedFont.Create(contextMenuStrip.Font.Size, contextMenuStrip.Font.Style);
            contextMenuStrip.Font = menuFont;
            foreach (ToolStripItem item in contextMenuStrip.Items)
                item.Font = EmbeddedFont.Create(item.Font.Size, item.Font.Style);
        }
    }
}
