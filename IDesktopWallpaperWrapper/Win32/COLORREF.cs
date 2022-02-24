using System.Runtime.InteropServices;

namespace IDesktopWallpaperWrapper.Win32
{
    /// <summary>
    /// The COLORREF value is used to specify an RGB color.
    /// </summary>
    /// <remarks>
    /// When specifying an explicit RGB color, the COLORREF value has the following hexadecimal form: 0x00bbggrr
    /// The low-order byte contains a value for the relative intensity of red; the second byte contains a value for green; and the third byte contains a value for blue.
    /// The high-order byte must be zero. The maximum value for a single byte is 0xFF.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct COLORREF
    {
        public uint ColorDWORD;

        public COLORREF(System.Drawing.Color color)
        {
            ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
        }

        public System.Drawing.Color GetColor()
        {
            return System.Drawing.Color.FromArgb((int)(0x000000FFU & ColorDWORD),
           (int)(0x0000FF00U & ColorDWORD) >> 8, (int)(0x00FF0000U & ColorDWORD) >> 16);
        }

        public void SetColor(System.Drawing.Color color)
        {
            ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
        }
    }
}
