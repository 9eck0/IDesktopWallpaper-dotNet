﻿using System;
using System.Runtime.InteropServices;

namespace IDesktopWallpaperWrapper.Win32
{
    /// <summary>
    /// Specifies the FMTID/PID identifier that programmatically identifies a property. Replaces SHCOLUMNID.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PROPERTYKEY
    {
        /// <summary>
        /// A unique GUID for the property.
        /// </summary>
        public Guid fmtid;
        /// <summary>
        /// A property identifier (PID). This parameter is not used as in SHCOLUMNID.
        /// It is recommended that you set this value to PID_FIRST_USABLE. Any value greater than or equal to 2 is acceptable.
        /// </summary>
        public uint pid;
    }
}
