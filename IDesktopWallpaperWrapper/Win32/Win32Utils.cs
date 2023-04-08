using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace IDesktopWallpaperWrapper.Win32
{
    [ComVisible(false)]
    public static class Win32Utils
    {
        #region COM IMPORTS

        // See https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-shcreateitemfromparsingname
        // See also https://stackoverflow.com/questions/42966489/how-to-use-shcreateitemfromparsingname-with-names-from-the-shell-namespace
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        private static extern void SHCreateItemFromParsingName(
            [MarshalAs(UnmanagedType.LPWStr)] string pszPath,
            IntPtr /*IBindCtx*/ pbc,
            [In][MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)] out IShellItem ppv);


        // See https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-shcreateshellitemarrayfromshellitem
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        private static extern void SHCreateShellItemArrayFromShellItem(
            [In][MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)] IShellItem psi,
            [In][MarshalAs(UnmanagedType.LPStruct)] Guid iIdIShellItem,
            [Out][MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)] out IShellItemArray iShellItemArray);

        #endregion


        /// <summary>
        /// Packages a single display name into an IShellItemArray container.
        /// </summary>
        /// <param name="item">The display name to be packaged.</param>
        /// <returns>The resulting IShellItemArray container.</returns>
        /// <remarks>
        /// When given an empty string, the IShellItem created will be linked to "This PC", a special (abstract) folder.
        /// </remarks>
        public static IShellItemArray CreateIShellItemArray(string item)
        {
            Guid IShellItemIID = new Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe");
            Guid IShellItemArrayIID = new Guid("B63EA76D-1F85-456F-A19C-48159EFA858B");

            // First we pack the item in an IShellItem container
            SHCreateItemFromParsingName(item, IntPtr.Zero, IShellItemIID, out IShellItem packedItem);

            // Then we pack the IShellItem into an IShellItemArray container
            SHCreateShellItemArrayFromShellItem(packedItem, IShellItemArrayIID, out IShellItemArray packedItemArray);

            return packedItemArray;
        }

        /// <summary>
        /// Extracts a specified type of display names from an IShellItemArray container.
        /// </summary>
        /// <param name="itemArray">The IShellItemArray containing the requested items.</param>
        /// <param name="displayNameType">The type of display name to retrieve.</param>
        /// <returns>A string array containing the requested display names extracted from the COM container.</returns>
        public static string[] ParseIShellItemArray(IShellItemArray itemArray, SIGDN displayNameType)
        {
            itemArray.GetCount(out uint itemCount);

            // Holds the file system paths associated with the slideshow items
            string[] results = new string[itemCount];

            for (uint i = 0; i < itemCount; i++)
            {
                itemArray.GetItemAt(i, out IShellItem item);

                // Obtains the requested display name of each IShellItem
                item.GetDisplayName(displayNameType, out StringBuilder absolutePathDisplay);

                results[i] = absolutePathDisplay.ToString();
            }

            return results;
        }

        public static bool IsDirectoryPath(string path)
        {
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(path);

            return attr.HasFlag(FileAttributes.Directory);
        }
    }
}
