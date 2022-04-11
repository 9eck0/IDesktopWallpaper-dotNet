using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IDesktopWallpaperWrapper;

namespace IDW_UnitTestProject
{
    [TestClass]
    public class DesktopWallpaperUnitTest
    {
        [TestMethod]
        public void TestGetWallpaper()
        {
            DesktopWallpaper desktopWallpaper = new DesktopWallpaper();

            // A call to GetActiveMonitorIDs() will test all monitor-related methods.
            string[] activeMonitorIDs = desktopWallpaper.GetActiveMonitorIDs();
            Assert.IsNotNull(activeMonitorIDs, "GetActiveMonitorIDs() returned a null array of monitor IDs");
            Assert.IsTrue(activeMonitorIDs.Length > 0, "GetActiveMonitorIDs() returned an empty array of monitor IDs");

            foreach (string monitorID in activeMonitorIDs) {
                string imageLocation = desktopWallpaper.GetWallpaper(monitorID);
                Assert.IsNotNull(imageLocation, "GetWallpaper() returned a null string path");
                Assert.IsTrue(imageLocation.Length > 0, "GetWallpaper() returned an empty string path");
            }
        }

        [TestMethod]
        public void TestGetSlideshow()
        {

        }
    }
}
