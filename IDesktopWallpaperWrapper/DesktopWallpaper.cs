using IDesktopWallpaperWrapper.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IDesktopWallpaperWrapper
{
    /// <summary>
    /// A wrapper object around the implementation coclass of Shobjidl_core::IDesktopWallpaper COM interface.
    /// </summary>
    /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-idesktopwallpaper"/>
    [ComVisible(true)]
    public class DesktopWallpaper
    {
        private Guid IDesktopWallpaperCoclassClsID = new Guid("c2cf3110-460e-4fc1-b9d0-8a1c0c9cc4bd");
        private IDesktopWallpaper wallpaperEngine;

        /// <summary>
        /// Instantiates the Windows desktop wallpaper engine.
        /// </summary>
        /// <remarks>
        /// This constructor calls the native (unmanaged) IDesktopWallpaper COM interface to obtain an instance of the coclass implementing said interface.
        /// </remarks>
        public DesktopWallpaper()
        {
            Type typeIDesktopWallpaper = Type.GetTypeFromCLSID(IDesktopWallpaperCoclassClsID);
            wallpaperEngine = (IDesktopWallpaper)Activator.CreateInstance(typeIDesktopWallpaper);
        }

        #region IDESKTOPWALLPAPER MONITOR METHODS

        /// <summary>
        /// Retrieves the unique ID of one of the system's monitors.
        /// </summary>
        /// <param name="monitorIndex">The number of the monitor. Call GetMonitorDevicePathCount to determine the total number of monitors.</param>
        /// <returns>The ID of the monitor at the specified index.</returns>
        /// <remarks>
        /// This method can be called on monitors that are currently detached but that have an image assigned to them.
        /// Call GetMonitorRECT to distinguish between attached and detached monitors.
        /// </remarks>
        /// <exception cref="COMException">Code E_FAIL is thrown monitorIndex is out of range.</exception>
        public string GetMonitorDevicePathAt(uint monitorIndex)
        {
            return wallpaperEngine.GetMonitorDevicePathAt(monitorIndex).ToString();
        }

        /// <summary>
        /// Retrieves the number of monitors that are associated with the system.
        /// </summary>
        /// <returns>The number of monitors, active or not.</returns>
        /// <remarks>
        /// The count retrieved through this method includes monitors that are currently detached but that have an image assigned to them.
        /// Call GetMonitorRECT to distinguish between attached and detached monitors.
        /// </remarks>
        public uint GetMonitorDevicePathCount()
        {
            return wallpaperEngine.GetMonitorDevicePathCount();
        }

        /// <summary>
        /// Retrieves the display rectangle of the specified monitor.
        /// </summary>
        /// <param name="monitorID">The ID of the monitor to query. You can get this value through GetMonitorDevicePathAt.</param>
        /// <returns>The display rectangle of the monitor specified by monitorID, in screen coordinates.</returns>
        /// <exception cref="ArgumentException">The ID supplied in monitorID cannot be found.</exception>
        /// <exception cref="ArgumentNullException">A null string is provided for monitorID.</exception>
        /// <exception cref="COMException">Code S_FALSE is thrown when trying to access a monitor not currently attached to the system.</exception>
        public Rectangle GetMonitorRECT(string monitorID)
        {
            if (monitorID == null)
            {
                throw new ArgumentNullException("monitorID", "A null string was provided.");
            }

            return wallpaperEngine.GetMonitorRECT(monitorID);
        }

        #endregion

        #region IDESKTOPWALLPAPER WALLPAPER METHODS

        /// <summary>
        /// Enables the desktop background.
        /// </summary>
        /// <remarks>
        /// This method would normally be called to disable the desktop background for performance reasons.
        /// When the desktop background is disabled, a solid color is shown in its place.
        /// To get or set the specific color, use the GetBackgroundColor and SetBackgroundColor methods.
        /// </remarks>
        public void EnableWallpaper()
        {
            wallpaperEngine.Enable(true);
        }

        /// <summary>
        /// Disables the desktop background.
        /// </summary>
        /// <remarks>
        /// This method would normally be called to disable the desktop background for performance reasons.
        /// When the desktop background is disabled, a solid color is shown in its place.
        /// To get or set the specific color, use the GetBackgroundColor and SetBackgroundColor methods.
        /// 
        /// Note: A call to the SetWallpaper or SetSlideshow methods will enable the desktop background even if it is currently disabled through this method.
        /// </remarks>
        public void DisableWallpaper()
        {
            wallpaperEngine.Enable(false);
        }

        /// <summary>
        /// Retrieves the color that is visible on the desktop when no image is displayed or when the desktop background has been disabled.
        /// This color is also used as a border when the desktop wallpaper does not fill the entire screen.
        /// </summary>
        /// <returns>The color of the background. If this method fails, returns Color.Empty</returns>
        public Color GetBackgroundColor()
        {
            uint colorref = wallpaperEngine.GetBackgroundColor();
            if (colorref == 0xffffffff)
            {
                return Color.Empty;
            }
            return ColorTranslator.FromWin32((int)colorref);
        }

        /// <summary>
        /// Retrieves the current display value for the desktop background image.
        /// </summary>
        /// <returns>A DESKTOP_WALLPAPER_POSITION enum value specifying how the image is being displayed on the system's monitors</returns>
        public DESKTOP_WALLPAPER_POSITION GetPosition()
        {
            return wallpaperEngine.GetPosition();
        }

        /// <summary>
        /// Gets the path to the current desktop wallpaper image file.
        /// </summary>
        /// <param name="monitorID">The ID of the monitor. This value can be obtained through GetMonitorDevicePathAt.
        /// This value can be set to null. In that case, if a single wallpaper image is displayed on all of the system's monitors, the method executes successfully.
        /// If this value is set to null and different monitors are displaying different wallpapers or a slideshow is running,
        /// the method returns an empty string.</param>
        /// <returns>
        /// The path to the wallpaper image file.
        /// This string will be empty if no wallpaper image is being displayed or if a monitor is displaying a solid color.
        /// </returns>
        /// <exception cref="ArgumentException">The ID supplied in monitorID cannot be found.</exception>
        /// <exception cref="COMException">Code S_FALSE is thrown when trying to access a monitor not currently attached to the system.</exception>
        public string GetWallpaper(string monitorID)
        {
            return wallpaperEngine.GetWallpaper(monitorID).ToString();
        }

        /// <summary>
        /// Sets the color that is visible on the desktop when no image is displayed or when the desktop background has been disabled.
        /// This color is also used as a border when the desktop wallpaper does not fill the entire screen.
        /// </summary>
        /// <param name="color">The desktop background color.</param>
        public void SetBackgroundColor(Color color)
        {
            uint colorref = (uint)ColorTranslator.ToWin32(color);
            wallpaperEngine.SetBackgroundColor(colorref);
        }

        /// <summary>
        /// Sets the display option for the desktop wallpaper image, determining whether the image should be centered, tiled, or stretched.
        /// </summary>
        /// <param name="position">One of the DESKTOP_WALLPAPER_POSITION enumeration values that specify how the image will be displayed on the system's monitors.</param>
        /// <remarks>
        /// This method will perform no action if the desktop wallpaper is already displayed as asked for in position.
        /// </remarks>
        public void SetPosition(DESKTOP_WALLPAPER_POSITION position)
        {
            wallpaperEngine.SetPosition(position);
        }

        /// <summary>
        /// Sets the desktop wallpaper.
        /// </summary>
        /// <param name="monitorID">The ID of the monitor. This value can be obtained through GetMonitorDevicePathAt. Set this value to null to set the wallpaper image on all monitors.</param>
        /// <param name="wallpaper">The full path of the wallpaper image file.</param>
        /// <exception cref="ArgumentException">The ID supplied in monitorID cannot be found.</exception>
        /// <exception cref="ArgumentException">The wallpaper path does not point to a valid image file.</exception>
        /// <exception cref="COMException">Code S_FALSE is thrown when trying to access a monitor not currently attached to the system.</exception>
        public void SetWallpaper(string monitorID, string wallpaper)
        {
            wallpaperEngine.SetWallpaper(monitorID, wallpaper);
        }

        #endregion

        #region IDESKTOPWALLPAPER SLIDESHOW METHODS

        // NOTE: commented code below has been deprecated in recent Windows versions,
        // due to the new wallpaper implementation with DWM (on Windows 10 and thereafter)
        // Below code will thus not work if monitorID is not null or direction is backwards;
        // this remains true even if shuffling is disabled.
        /*
        /// <summary>
        /// Switches the wallpaper on a specified monitor to the next image in the slideshow.
        /// </summary>
        /// <param name="monitorID">The ID of the monitor on which to change the wallpaper image.
        /// This ID can be obtained through the GetMonitorDevicePathAt method.
        /// If this parameter is set to null, the monitor scheduled to change next is used.</param>
        /// <param name="direction">The direction that the slideshow should advance.</param>
        /// <exception cref="NotImplementedException">The coclass implementing IDesktopWallpaper has not specified an implementation for this method.</exception>
        public void AdvanceSlideshow(string monitorID, DESKTOP_SLIDESHOW_DIRECTION direction)
        {
            wallpaperEngine.AdvanceSlideshow(monitorID, direction);
        }
        */

        /// <summary>
        /// Switches the wallpaper on the next scheduled monitor to the next image in the slideshow.
        /// </summary>
        /// <remarks>
        /// On a multi-monitor setup, the order of slideshow advancing follows an alternating pattern determined by the DWM.
        /// </remarks>
        public void AdvanceSlideshow()
        {
            wallpaperEngine.AdvanceSlideshow(null, DESKTOP_SLIDESHOW_DIRECTION.DSD_FORWARD);
        }

        /// <summary>
        /// Gets the path to the directory where the slideshow images are stored.
        /// </summary>
        /// <returns>
        /// An array of strings representing the file system paths associated with the slideshow items.
        /// This array can contain individual items stored in the same container, or it can contain a single item which is the container itself.
        /// </returns>
        public string[] GetSlideshow()
        {
            IShellItemArray slideshows = wallpaperEngine.GetSlideshow();
            return Win32Utils.ParseIShellItemArray(slideshows, SIGDN.FILESYSPATH);
        }

        /// <summary>
        /// Gets the current status of the slideshow.
        /// </summary>
        /// <returns>One or more DESKTOP_SLIDESHOW_STATE flags. Use the returned enum item's HasFlag method to determine whether a particular flag is included.</returns>
        public DESKTOP_SLIDESHOW_STATE GetSlideshowStatus()
        {
            return wallpaperEngine.GetStatus();
        }

        /// <summary>
        /// Gets the interval between slideshow image transitions, in milliseconds.
        /// </summary>
        /// <returns>An unsigned integer indicating the interval, in milliseconds, between slideshow transitions.</returns>
        public uint GetSlideshowTransitionInterval()
        {
            wallpaperEngine.GetSlideshowOptions(out _, out uint slideshowTick);
            return slideshowTick;
        }

        /// <summary>
        /// Gets the current desktop wallpaper slideshow setting for shuffle.
        /// </summary>
        /// <returns>A boolean indicating whether shuffle is disabled or enabled.</returns>
        public bool IsSlideshowShufflingEnabled()
        {
            wallpaperEngine.GetSlideshowOptions(out uint options, out uint _);
            return options != 0;
        }

        /// <summary>
        /// Specifies the images to use for the desktop wallpaper slideshow.
        /// </summary>
        /// <param name="directory">The full path to the directory containing the slideshow images.</param>
        /// <exception cref="System.IO.FileNotFoundException">If the provided slideshow folder is invalid.</exception>
        public void SetSlideshow(string directory)
        {
            // possible exceptions: not a valid path format, not an existing folder
            directory = System.IO.Path.GetFullPath(directory);
            wallpaperEngine.SetSlideshow(Win32.Win32Utils.CreateIShellItemArray(directory));
        }

        /// <summary>
        /// Sets the current desktop wallpaper slideshow setting for shuffle.
        /// </summary>
        /// <param name="enable">Whether to enable shuffling or not.</param>
        public void SetSlideshowShuffle(bool enable)
        {
            wallpaperEngine.GetSlideshowOptions(out uint _, out uint currentInterval);
            wallpaperEngine.SetSlideshowOptions(enable ? 1u : 0u, currentInterval);
        }

        /// <summary>
        /// Sets the interval between slideshow image transitions, in milliseconds.
        /// </summary>
        /// <param name="millisInterval">The interval, in milliseconds, between slideshow transitions.</param>
        public void SetSlideshowTransitionInterval(uint millisInterval)
        {
            wallpaperEngine.GetSlideshowOptions(out uint currentShuffleStatus, out uint _);
            wallpaperEngine.SetSlideshowOptions(currentShuffleStatus, millisInterval);
        }

        #endregion

        #region UTILITY METHODS

        /// <summary>
        /// Obtains the monitor IDs of all monitors currently online.
        /// </summary>
        /// <returns>A string array containing the IDs of all active monitors.</returns>
        public string[] GetActiveMonitorIDs()
        {
            uint monitorPathCount = GetMonitorDevicePathCount();
            List<string> results = new List<string>((int)monitorPathCount);
            for (uint i = 0; i < monitorPathCount; i++)
            {
                try
                {
                    string monitorID = GetMonitorDevicePathAt(i);
                    GetMonitorRECT(monitorID);
                    results.Add(monitorID);
                }
                catch (COMException)
                {
                    // This monitor is currently offline
                }
            }
            return results.ToArray();
        }

        /// <summary>
        /// Obtains the monitor IDs of all monitors saved in the system.
        /// </summary>
        /// <returns>A string array containing the IDs of all saved monitors.</returns>
        /// <remarks>
        /// To obtain all monitors that are online, see <see cref="GetActiveMonitorIDs"/>.
        /// </remarks>
        public string[] GetAllMonitorIDs()
        {
            uint monitorPathCount = GetMonitorDevicePathCount();
            List<string> results = new List<string>((int)monitorPathCount);
            for (uint i = 0; i < monitorPathCount; i++)
            {
                string monitorID = GetMonitorDevicePathAt(i);
                results.Add(monitorID);
            }
            return results.ToArray();
        }

        /// <summary>
        /// Gets the path to the directory in which the active slideshow images are stored.
        /// </summary>
        /// <returns>The path to the active slideshow, or <c>null</c> if no slideshow is configured.</returns>
        public string GetSlideshowFolder()
        {
            if (!GetSlideshowStatus().HasFlag(DESKTOP_SLIDESHOW_STATE.DSS_SLIDESHOW))
            {
                return null;
            }

            string[] slideshowItems = GetSlideshow();

            if (slideshowItems.Length == 0)
            {
                return null;
            }
            else if (Win32Utils.IsDirectoryPath(slideshowItems[0]))
            {
                return slideshowItems[0];
            }
            else
            {
                return System.IO.Path.GetDirectoryName(slideshowItems[0]);
            }
        }

        #endregion
    }
}
