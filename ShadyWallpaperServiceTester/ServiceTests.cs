using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadyWallpaperService;

namespace ShadyWallpaperServiceTester
{
    [TestClass]
    public class ServiceTests
    {
        [TestMethod]
        public void TestConnection()
        {
            IShadyWallpaperService service = new TestingService();
        }
    }
}
