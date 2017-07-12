using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeranfuenMirrorSyncLib.Controllers;

namespace SeranfuenMirrorSyncLibTests
{
    [TestClass]
    public class TestFileDatabaseEntry
    {
        [TestMethod]
        public void TestGetVirtual_RootGiven_EmptyReturned()
        {
            var result = FileDatabaseEntry.GetVirtualPath(@"C:\Foo\", @"C:\Foo");
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void TestGetVirtual_SubpathGiven()
        {
            var result = FileDatabaseEntry.GetVirtualPath(@"C:\Foo\bar\c.txt", @"C:\Foo");
            Assert.AreEqual(@"bar\c.txt", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetVirtual_RootGivenNotMatching_Exception()
        {
            var result = FileDatabaseEntry.GetVirtualPath(@"C:\Foo\bar", @"C:\Bar\");
        }
    }
}
