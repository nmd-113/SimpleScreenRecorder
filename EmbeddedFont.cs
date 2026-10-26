using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SimpleScreenRecorder
{
    internal static class EmbeddedFont
    {
        private static readonly PrivateFontCollection FontCollection;
        private static readonly FontFamily UiFamily;
        private static readonly Dictionary<string, Font> FontCache = new Dictionary<string, Font>();
        private static IntPtr _fontData;
        private static IntPtr _gdiFontHandle;

        static EmbeddedFont()
        {
            try
            {
                FontCollection = new PrivateFontCollection();

                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "SimpleScreenRecorder.Resources.JetBrainsMono-Regular.ttf"))
                {
                    if (stream == null)
                        throw new FileNotFoundException("Embedded JetBrains Mono font was not found.");

                    using (var buffer = new MemoryStream())
                    {
                        stream.CopyTo(buffer);
                        byte[] fontBytes = buffer.ToArray();
                        _fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
                        Marshal.Copy(fontBytes, 0, _fontData, fontBytes.Length);

                        // WinForms controls commonly render through GDI. A PrivateFontCollection
                        // alone only makes this font available to GDI+, so GDI may substitute a
                        // different family for native controls such as buttons and combo boxes.
                        uint registeredFonts;
                        _gdiFontHandle = AddFontMemResourceEx(
                            _fontData, (uint)fontBytes.Length, IntPtr.Zero, out registeredFonts);
                        if (_gdiFontHandle == IntPtr.Zero || registeredFonts == 0)
                            throw new InvalidOperationException("JetBrains Mono could not be registered for WinForms controls.");

                        FontCollection.AddMemoryFont(_fontData, fontBytes.Length);
                    }
                }

                foreach (FontFamily family in FontCollection.Families)
                {
                    if (string.Equals(family.Name, "JetBrains Mono", StringComparison.OrdinalIgnoreCase))
                    {
                        UiFamily = family;
                        break;
                    }
                }

                if (UiFamily == null)
                    throw new InvalidOperationException("JetBrains Mono family could not be loaded.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Embedded JetBrains Mono could not be loaded: " + ex);

                if (_fontData != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(_fontData);
                    _fontData = IntPtr.Zero;
                }

                if (FontCollection != null)
                    FontCollection.Dispose();

                UiFamily = FontFamily.GenericMonospace;
            }
        }

        public static Font Create(float size, FontStyle style)
        {
            string key = size.ToString(System.Globalization.CultureInfo.InvariantCulture) + ":" + (int)style;
            if (!FontCache.TryGetValue(key, out Font font))
            {
                font = new Font(UiFamily, size, style, GraphicsUnit.Point);
                FontCache.Add(key, font);
            }

            return font;
        }

        public static void Apply(Control control, float scale = 1F)
        {
            if (control == null)
                return;

            float size = control.Font.Size * scale;
            FontStyle style = control.Font.Style;
            control.Font = Create(size, style);

            foreach (Control child in control.Controls)
                Apply(child, scale);
        }

        [DllImport("gdi32.dll", EntryPoint = "AddFontMemResourceEx", SetLastError = true)]
        private static extern IntPtr AddFontMemResourceEx(
            IntPtr fontData,
            uint fontDataSize,
            IntPtr reserved,
            out uint numberOfFonts);
    }
}
