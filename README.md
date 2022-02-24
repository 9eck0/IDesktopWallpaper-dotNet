# IDesktopWallpaper-dotNet
A fully-functional C# wrapper around Win32 API's IDesktopWallpaper interface.
IDesktopWallpaper is a COM interface introduced in Windows 8 as part of the Win32 API, permitting better control of desktop wallpapers on a multi-monitor system.

# Usage
Usage of IDesktopWallpaper requires a Windows 8 or later machine.
To start, instantiate a DesktopWallpaper class, which is the wrapper class around the COM interface.

All functionalities related to the scope of wallpaper and slideshow manipulations are available, to the extent provided by IDesktopWallpaper's associated coclass implementation.
The coclass implementation of [AdvanceSlideshow](https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-idesktopwallpaper-advanceslideshow) method has been restricted in functionality, not being able to specify a particular monitor, nor advancing backwards in direction.<br/>
More implementation restrictions suci as this one are possible, depending on the version of Windows this is run on.

# TODO
- making this repo more presentable after this initial creation phase
- package the code base into a Visual Studio project
