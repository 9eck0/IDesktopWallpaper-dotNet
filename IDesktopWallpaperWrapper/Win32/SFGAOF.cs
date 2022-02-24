using System;

namespace IDesktopWallpaperWrapper.Win32
{
    /// <summary>
    /// Attributes that can be retrieved on an item (file or folder) or set of items.
    /// </summary>
    [Flags]
    public enum SFGAOF : uint
    {
        /// <summary>
        /// The specified items can be copied.
        /// </summary>
        SFGAO_CANCOPY = 0x1,                // Objects can be copied    (DROPEFFECT_COPY)
        /// <summary>
        /// The specified items can be moved.
        /// </summary>
        SFGAO_CANMOVE = 0x2,                // Objects can be moved     (DROPEFFECT_MOVE)
        /// <summary>
        /// Shortcuts can be created for the specified items.
        /// </summary>
        SFGAO_CANLINK = 0x4,                // Objects can be linked    (DROPEFFECT_LINK)
        /// <summary>
        /// The specified items can be bound to an IStorage object through IShellFolder::BindToObject.
        /// </summary>
        SFGAO_STORAGE = 0x00000008,         // supports BindToObject(IID_IStorage)
        /// <summary>
        /// The specified items can be renamed.
        /// </summary>
        SFGAO_CANRENAME = 0x00000010,         // Objects can be renamed
        /// <summary>
        /// The specified items can be deleted.
        /// </summary>
        SFGAO_CANDELETE = 0x00000020,         // Objects can be deleted
        /// <summary>
        /// The specified items have property sheets.
        /// </summary>
        SFGAO_HASPROPSHEET = 0x00000040,         // Objects have property sheets
        /// <summary>
        /// The specified items are drop targets.
        /// </summary>
        SFGAO_DROPTARGET = 0x00000100,         // Objects are drop target
        /// <summary>
        /// This flag is a mask for the capability attributes:
        /// SFGAO_CANCOPY, SFGAO_CANMOVE, SFGAO_CANLINK, SFGAO_CANRENAME, SFGAO_CANDELETE, SFGAO_HASPROPSHEET, and SFGAO_DROPTARGET.
        /// Callers normally do not use this value.
        /// </summary>
        SFGAO_CAPABILITYMASK = 0x00000177,
        /// <summary>
        /// The specified items are encrypted and might require special presentation.
        /// </summary>
        SFGAO_ENCRYPTED = 0x00002000,         // object is encrypted (use alt color)
        /// <summary>
        /// Accessing the item (through IStream or other storage interfaces) is expected to be a slow operation.
        /// Applications should avoid accessing items flagged with SFGAO_ISSLOW. 
        /// </summary>
        SFGAO_ISSLOW = 0x00004000,         // 'slow' object
        /// <summary>
        /// The specified items are shown as dimmed and unavailable to the user.
        /// </summary>
        SFGAO_GHOSTED = 0x00008000,         // ghosted icon
        /// <summary>
        /// The specified items are shortcuts.
        /// </summary>
        SFGAO_LINK = 0x00010000,         // Shortcut (link)
        /// <summary>
        /// The specified objects are shared.
        /// </summary>
        SFGAO_SHARE = 0x00020000,         // shared
        /// <summary>
        /// The specified items are read-only.
        /// </summary>
        SFGAO_READONLY = 0x00040000,         // read-only
        /// <summary>
        /// The item is hidden and should not be displayed unless the Show hidden files and folders option is enabled in Folder Settings.
        /// </summary>
        SFGAO_HIDDEN = 0x00080000,         // hidden object
        /// <summary>
        /// Do not use.
        /// </summary>
        SFGAO_DISPLAYATTRMASK = 0x000FC000,
        /// <summary>
        /// The specified folders are either file system folders or contain at least one descendant
        /// (child, grandchild, or later) that is a file system (SFGAO_FILESYSTEM) folder.
        /// </summary>
        SFGAO_FILESYSANCESTOR = 0x10000000,         // may contain children with SFGAO_FILESYSTEM
        /// <summary>
        /// The specified items are folders.
        /// </summary>
        SFGAO_FOLDER = 0x20000000,         // support BindToObject(IID_IShellFolder)
        /// <summary>
        /// The specified folders or files are part of the file system (that is, they are files, directories, or root directories).
        /// The parsed names of the items can be assumed to be valid Win32 file system paths.
        /// These paths can be either UNC or drive-letter based.
        /// </summary>
        SFGAO_FILESYSTEM = 0x40000000,         // is a win32 file system object (file/folder/root)
        /// <summary>
        /// The specified folders have subfolders.
        /// </summary>
        SFGAO_HASSUBFOLDER = 0x80000000,         // may contain children with SFGAO_FOLDER
        /// <summary>
        /// This flag is a mask for content attributes, at present only SFGAO_HASSUBFOLDER.
        /// Callers normally do not use this value.
        /// </summary>
        SFGAO_CONTENTSMASK = 0x80000000,
        /// <summary>
        /// When specified as input, SFGAO_VALIDATE instructs the folder to validate that the items contained in a folder or Shell item array exist.
        /// If one or more of those items do not exist, IShellFolder::GetAttributesOf and IShellItemArray::GetAttributes return a failure code.
        /// This flag is never returned as an [out] value.
        /// </summary>
        SFGAO_VALIDATE = 0x01000000,         // invalidate cached information
        /// <summary>
        /// The specified items are on removable media or are themselves removable devices.
        /// </summary>
        SFGAO_REMOVABLE = 0x02000000,         // is this removeable media?
        /// <summary>
        /// The specified items are compressed.
        /// </summary>
        SFGAO_COMPRESSED = 0x04000000,         // Object is compressed (use alt color)
        /// <summary>
        /// The specified items can be hosted inside a web browser or Windows Explorer frame.
        /// </summary>
        SFGAO_BROWSABLE = 0x08000000,         // supports IShellFolder, but only implements CreateViewObject() (non-folder view)
        /// <summary>
        /// The items are nonenumerated items and should be hidden.
        /// They are not returned through an enumerator such as that created by the IShellFolder::EnumObjects method.
        /// </summary>
        SFGAO_NONENUMERATED = 0x00100000,         // is a non-enumerated object
        /// <summary>
        /// The items contain new content, as defined by the particular application.
        /// </summary>
        SFGAO_NEWCONTENT = 0x00200000,         // should show bold in explorer tree
        /// <summary>
        /// Not supported.
        /// </summary>
        SFGAO_CANMONIKER = 0x00400000,         // defunct
        /// <summary>
        /// Not supported.
        /// </summary>
        SFGAO_HASSTORAGE = 0x00400000,         // defunct
        /// <summary>
        /// Indicates that the item has a stream associated with it.
        /// That stream can be accessed through a call to IShellFolder::BindToObject or IShellItem::BindToHandler with IID_IStream in the riid parameter.
        /// </summary>
        SFGAO_STREAM = 0x00400000,         // supports BindToObject(IID_IStream)
        /// <summary>
        /// Children of this item are accessible through IStream or IStorage.
        /// Those children are flagged with SFGAO_STORAGE or SFGAO_STREAM.
        /// </summary>
        SFGAO_STORAGEANCESTOR = 0x00800000,         // may contain children with SFGAO_STORAGE or SFGAO_STREAM
        /// <summary>
        /// This flag is a mask for the storage capability attributes:
        /// SFGAO_STORAGE, SFGAO_LINK, SFGAO_READONLY, SFGAO_STREAM, SFGAO_STORAGEANCESTOR, SFGAO_FILESYSANCESTOR, SFGAO_FOLDER, and SFGAO_FILESYSTEM.
        /// Callers normally do not use this value.
        /// </summary>
        SFGAO_STORAGECAPMASK = 0x70C50008,         // for determining storage capabilities, ie for open/save semantics
    }
}
