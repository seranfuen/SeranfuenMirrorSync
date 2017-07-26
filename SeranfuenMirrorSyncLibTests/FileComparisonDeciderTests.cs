using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeranfuenMirrorSyncLib.Controllers;
using static SeranfuenMirrorSyncLib.Data.FolderSyncAction;
using SeranfuenMirrorSyncLib.Data;

namespace SeranfuenMirrorSyncLibTests
{
    /// <summary>
    /// Summary description for FileComparisonDecider
    /// </summary>
    [TestClass]
    public class FileComparisonDeciderTests
    {
        public FileComparisonDeciderTests()
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
        public void SameSizeDate()
        {
            var file1 = InitFooEntrySource();
            var file2 = InitFooEntryMirror();

            file1.Size = file2.Size = 1024;
            file1.LastModificationDate = file2.LastModificationDate = new DateTime(2017, 5, 10);

            var decider = InitDecider();
            var action = decider.DecideAction(file1, file2);
            Assert.AreEqual(FileSyncAction.FileActionType.Skip, action.Action);
        }

        [TestMethod]
        public void DifferentSizeDate()
        {
            var file1 = InitFooEntrySource();
            var file2 = InitFooEntryMirror();

            file1.Size = 1024;
            file2.Size = 4028;
            file1.LastModificationDate = file2.LastModificationDate = new DateTime(2017, 5, 10);

            var decider = InitDecider();
            var action = decider.DecideAction(file1, file2);
            Assert.AreEqual(FileSyncAction.FileActionType.Copy, action.Action);
        }

        [TestMethod]
        public void SameSizeDifferentDate()
        {
            var file1 = InitFooEntrySource();
            var file2 = InitFooEntryMirror();

            file1.Size = file2.Size = 1024;
            file1.LastModificationDate = new DateTime(2017, 5, 10);
            file2.LastModificationDate = new DateTime(2017, 6, 1);

            var decider = InitDecider();
            var action = decider.DecideAction(file1, file2);
            Assert.AreEqual(FileSyncAction.FileActionType.Copy, action.Action);
        }

        [TestMethod]
        public void File2NotExists()
        {
            var file1 = InitFooEntrySource();

            file1.Size = 1024;
            file1.LastModificationDate = new DateTime(2017, 5, 10);

            var decider = InitDecider();
            var action = decider.DecideAction(file1, FileDatabaseEntry.NonExistentFile);
            Assert.AreEqual(FileSyncAction.FileActionType.Copy, action.Action);
        }

        [TestMethod]
        public void File1NotExists()
        {
            var file2 = InitFooEntryMirror();

            file2.Size = 1024;
            file2.LastModificationDate = new DateTime(2017, 6, 1);

            var decider = InitDecider();
            var action = decider.DecideAction(FileDatabaseEntry.NonExistentFile, file2);
            Assert.AreEqual(FileSyncAction.FileActionType.Delete, action.Action);
        }

        private string SourceRoot
        {
            get
            {
                return @"C:\a";
            }
        }

        private string MirrorRoot
        {
            get
            {
                return @"C:\bar";
            }
        }

        private FileDatabaseEntry InitFooEntrySource()
        {
            return new FileDatabaseEntry()
            {
                VirtualPath = "foo.txt",
                LocalPath = FileDatabaseEntry.GetLocalPath("foo.txt", SourceRoot)
            };
        }

        private FileDatabaseEntry InitFooEntryMirror()
        {
            return new FileDatabaseEntry()
            {
                VirtualPath = "foo.txt",
                LocalPath = FileDatabaseEntry.GetLocalPath("foo.txt", MirrorRoot)
            };
        }

        private FileComparisonDecider InitDecider()
        {
            return new FileComparisonDecider(SourceRoot, MirrorRoot);
        }
    }
}
