using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeranfuenMirrorSyncLib.Controllers;

namespace SeranfuenMirrorSyncLibTests
{
    /// <summary>
    /// Summary description for FileFilterTests
    /// </summary>
    [TestClass]
    public class FileFilterTests
    {
        public FileFilterTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ExtensionFileFilter_NoExtension()
        {
            var fileFilter = new ExtensionFilter(".jpg");
            var result = fileFilter.ShouldFilter(@"C:\foo\bar");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ExtensionFileFilter_DifferentExtension()
        {
            var fileFilter = new ExtensionFilter("jpg");
            var result = fileFilter.ShouldFilter(@"C:\foo\bar.txt");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ExtensionFileFilter_SameExtension()
        {
            var fileFilter = new ExtensionFilter("jpg");
            var result = fileFilter.ShouldFilter(@"C:\foo\bar.jpg");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ExtensionFileFilter_SubextensionDifferent()
        {
            var fileFilter = new ExtensionFilter("jpg");
            var result = fileFilter.ShouldFilter(@"C:\foo\bar.jpged");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegexFileNameFilter_NoFileName()
        {
            var fileFilter = new RegexFileNameFilter(@"^~.*");
            var result = fileFilter.ShouldFilter(@"C:\foo\bar\");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegexFileNameFilter_PathMatchingNotFileName()
        {
            var fileFilter = new RegexFileNameFilter(@"^~.*");
            var result = fileFilter.ShouldFilter(@"C:\~foo\bar.txt");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegexFileNameFilter_FileNameMatching()
        {
            var fileFilter = new RegexFileNameFilter(@"^~.*");
            var result = fileFilter.ShouldFilter(@"C:\foo\~bar.txt");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RegexFileNameFilter_FileNameAndExtensionNotMatching()
        {
            var fileFilter = new RegexFileNameFilter(@"^~.*\.xls");
            var result = fileFilter.ShouldFilter(@"C:\foo\~bar.txt");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegexFileNameFilter_FileNameAndExtensionMatching()
        {
            var fileFilter = new RegexFileNameFilter(@"^~.*\.xls");
            var result = fileFilter.ShouldFilter(@"C:\foo\~bar.xls");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RegexDirectoryPathFilter_NotMatching()
        {
            var fileFilter = new DirectoryPathRegexFilter(@"^C:\\Foo\\Bar");
            var result = fileFilter.ShouldFilter(@"C:\foo\spam");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegexDirectoryPathFilter_Matching()
        {
            var fileFilter = new DirectoryPathRegexFilter(@"^C:\\Foo\\Bar\\");
            var result = fileFilter.ShouldFilter(@"C:\foo\bar");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RegexDirectoryPathFilter_MatchingLastPart()
        {
            var fileFilter = new DirectoryPathRegexFilter(@"\\Spam$");
            var result = fileFilter.ShouldFilter(@"^C:\Foo\Bar\Spam");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RegexDirectoryPathFilter_NotMatchingLastPart()
        {
            var fileFilter = new DirectoryPathRegexFilter(@"\\Spam$");
            var result = fileFilter.ShouldFilter(@"^C:\Foo\Bar\Spam\Eggs");
            Assert.IsFalse(result);
        }
    }
}
