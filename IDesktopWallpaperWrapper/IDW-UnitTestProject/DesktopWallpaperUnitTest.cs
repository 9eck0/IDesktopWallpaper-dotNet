using Microsoft.VisualStudio.TestTools.UnitTesting;
using IDesktopWallpaperWrapper;
using IDesktopWallpaperWrapper.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IDesktopWallpaperWrapper.Tests
{
    [TestClass()]
    public class DesktopWallpaperUnitTest
    {
        private DesktopWallpaper _desktopWallpaper;
        private DesktopWallpaper GetInterface()
        {
            if (_desktopWallpaper == null) { _desktopWallpaper = new DesktopWallpaper(); }
            return _desktopWallpaper;
        }

        [TestMethod()]
        public void GetWallpaperTest()
        {
            var dw = GetInterface();

            // A call to GetActiveMonitorIDs() will test all monitor-related methods.
            string[] activeMonitorIDs = dw.GetActiveMonitorIDs();
            Assert.IsNotNull(activeMonitorIDs, "GetActiveMonitorIDs() returned a null array of monitor IDs");
            Assert.IsTrue(activeMonitorIDs.Length > 0, "GetActiveMonitorIDs() returned an empty array of monitor IDs");

            foreach (string monitorID in activeMonitorIDs)
            {
                string imageLocation = dw.GetWallpaper(monitorID);
                Assert.IsNotNull(imageLocation, "GetWallpaper() returned a null string path");
                Assert.IsTrue(imageLocation.Length > 0, "GetWallpaper() returned an empty string path");
            }
        }

        [TestMethod()]
        public void GetSlideshowTest()
        {
            var dw = GetInterface();
            string[] slideshowDir = dw.GetSlideshow();
            if (dw.GetSlideshowStatus() == DESKTOP_SLIDESHOW_STATE.DSS_SLIDESHOW)
            {
                Assert.IsTrue(Directory.Exists(slideshowDir[0]) || File.Exists(slideshowDir[0]));
                //Console.WriteLine("GetSlideshow: ", string.Join(" ", slideshowDir));
            }
        }
        [TestMethod()]
        public void SetSlideshowShuffleTest()
        {
            var dw = GetInterface();
            bool initialShuffle = dw.IsSlideshowShufflingEnabled();

            dw.SetSlideshowShuffle(!initialShuffle);
            dw.SetSlideshowShuffle(initialShuffle);
        }

        [TestMethod()]
        public void GetSlideshowFolderTest()
        {
            var dw = GetInterface();
            if (dw.GetSlideshowStatus() == DESKTOP_SLIDESHOW_STATE.DSS_SLIDESHOW)
            {
                dw.GetSlideshowFolder();
            }
            else {
                Assert.IsNull(dw.GetSlideshowFolder());
            }
        }
    }
}
