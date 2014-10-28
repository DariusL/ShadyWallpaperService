﻿//#define REWRITE
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadyWallpaperService;
using ShadyWallpaperService.DataTypes;
using System.Runtime.CompilerServices;

namespace ShadyWallpaperServiceTester
{
    [TestClass]
    public class ServiceTests
    {
        private const string path = @"..\..\SerializedTests\";
        private static IShadyWallpaperService GetService()
        {
            return new TestingService();
        }
        [TestMethod]
        public void TestConnection()
        {
            var service = GetService();
        }

        [TestMethod]
        public void Test1080pFromBoard()
        {
            var service = GetService();
            var data = service.BoardWalls("wg", "1", "R1920By1080", null);
            Test(data);
        }

        private static void Test<T>(T data, [CallerMemberName] string name = "")
        {
            var file = path + name;
            string result = Serializer.Serialize(data);
#if REWRITE
            System.IO.File.WriteAllText(file, result);
#else
                string expected = "";
                try
                {
                    expected = System.IO.File.ReadAllText(file);
                }
                catch { }
                Assert.AreEqual(expected, result);
#endif
        }
    }
}