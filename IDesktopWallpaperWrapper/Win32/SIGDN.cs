namespace IDesktopWallpaperWrapper.Win32
{
    /// <summary>
    /// Requests the form of an item's display name to retrieve through IShellItem::GetDisplayName and SHGetNameFromIDList.
    /// </summary>
    public enum SIGDN : uint
    {
        /// <summary>
        /// Returns the display name relative to the parent folder.
        /// </summary>
        NORMALDISPLAY = 0,
        /// <summary>
        /// Returns the parsing name relative to the parent folder.
        /// </summary>
        PARENTRELATIVEPARSING = 0x80018001,
        /// <summary>
        /// Returns the parsing name relative to the desktop.
        /// </summary>
        DESKTOPABSOLUTEPARSING = 0x80028000,
        /// <summary>
        /// Returns the editing name relative to the parent folder.
        /// </summary>
        PARENTRELATIVEEDITING = 0x80031001,
        /// <summary>
        /// Returns the editing name relative to the desktop.
        /// </summary>
        DESKTOPABSOLUTEEDITING = 0x8004c000,
        /// <summary>
        /// Returns the item's file system path, if it has one.
        /// </summary>
        FILESYSPATH = 0x80058000,
        /// <summary>
        /// Returns the item's URL, if it has one.
        /// </summary>
        URL = 0x80068000,
        /// <summary>
        /// Returns the path relative to the parent folder in a friendly format as displayed in an address bar.
        /// </summary>
        PARENTRELATIVEFORADDRESSBAR = 0x8001c001,
        /// <summary>
        /// Returns the path relative to the parent folder.
        /// </summary>
        PARENTRELATIVE = 0x80080001,
        /// <summary>
        /// Introduced in Windows 8.
        /// </summary>
        PARENTRELATIVEFORUI = 0x80094001

    }
}
