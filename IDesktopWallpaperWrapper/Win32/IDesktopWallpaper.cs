using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace IDesktopWallpaperWrapper.Win32
{
    /// <summary>
    /// The direction that the slideshow should advance.
    /// </summary>
    [ComVisible(true)]
    public enum DESKTOP_SLIDESHOW_DIRECTION
    {
        /// <summary>
        /// Advance the slideshow forward.
        /// </summary>
        DSD_FORWARD = 0,
        /// <summary>
        /// Advance the slideshow backward.
        /// </summary>
        DSD_BACKWARD = 1
    }



    /// <summary>
    /// Specifies how the desktop wallpaper should be displayed.
    /// </summary>
    [ComVisible(true)]
    public enum DESKTOP_WALLPAPER_POSITION
    {
        /// <summary>
        /// Center the image; do not stretch.
        /// </summary>
        DWPOS_CENTER = 0,
        /// <summary>
        /// Tile the image across all monitors.
        /// </summary>
        DWPOS_TILE = 1,
        /// <summary>
        /// Stretch the image to exactly fit on the monitor.
        /// </summary>
        DWPOS_STRETCH = 2,
        /// <summary>
        /// Stretch the image to exactly the height or width of the monitor without changing its aspect ratio or cropping the image.
        /// This can result in colored letterbox bars on either side or on above and below of the image.
        /// </summary>
        DWPOS_FIT = 3,
        /// <summary>
        /// Stretch the image to fill the screen, cropping the image as necessary to avoid letterbox bars.
        /// </summary>
        DWPOS_FILL = 4,
        /// <summary>
        /// Spans a single image across all monitors attached to the system.
        /// </summary>
        DWPOS_SPAN = 5
    }



    [ComVisible(true)]
    [Flags]
    public enum DESKTOP_SLIDESHOW_STATE
    {
        /// <summary>
        /// Slideshows are enabled.
        /// </summary>
        DSS_ENABLED = 1,
        /// <summary>
        /// A slideshow is currently configured.
        /// </summary>
        DSS_SLIDESHOW = 2,
        /// <summary>
        /// A remote session has temporarily disabled the slideshow.
        /// </summary>
        DSS_DISABLED_BY_REMOTE_SESSION = 4
    }



    /// <summary>
    /// COM interface providing methods for managing the desktop wallpaper.
    /// Instantiate this COM interface with DesktopWallpaper wrapper class instead.
    /// </summary>
    /// <remarks>
    /// Windows 8 or later is required in order to access this interface.
    /// </remarks>
    [ComImport]
    [Guid("b92b56a9-8b55-4e14-9a89-0199bbb6f93b")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDesktopWallpaper
    {
        //  As with any COM import, method ordering is crucial to the functioning of the interface.
        // The code ordering below must be maintained, else an AccessViolationException can be thrown.

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetWallpaper([MarshalAs(UnmanagedType.LPWStr)] string monitorID, [MarshalAs(UnmanagedType.LPWStr)] string wallpaper);

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        StringBuilder GetWallpaper([MarshalAs(UnmanagedType.LPWStr)] string monitorID);

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        StringBuilder GetMonitorDevicePathAt(uint monitorIndex);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        uint GetMonitorDevicePathCount();

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        RECT GetMonitorRECT([MarshalAs(UnmanagedType.LPWStr)] string monitorID);

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetBackgroundColor(uint color);

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        uint GetBackgroundColor();

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetPosition([MarshalAs(UnmanagedType.I4)] DESKTOP_WALLPAPER_POSITION position);

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.I4)]
        DESKTOP_WALLPAPER_POSITION GetPosition();

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetSlideshow(IShellItemArray items);
        //void SetSlideshow(IntPtr items);

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        IShellItemArray GetSlideshow();
        //IntPtr GetSlideshow();

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetSlideshowOptions(uint options, uint slideshowTick);

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetSlideshowOptions(out uint options, out uint slideshowTick);

        // NOT IMPLEMENTED BY COCLASS
        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AdvanceSlideshow([MarshalAs(UnmanagedType.LPWStr)] string monitorID, [MarshalAs(UnmanagedType.I4)] DESKTOP_SLIDESHOW_DIRECTION direction);

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        DESKTOP_SLIDESHOW_STATE GetStatus();

        //[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Enable([MarshalAs(UnmanagedType.Bool)] bool enable);
    }
}
