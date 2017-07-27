using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeranfuenMirrorSyncLib.Data;

namespace SeranfuenMirrorSyncLibTests
{
    /// <summary>
    /// Summary description for FileDatabaseTests
    /// </summary>
    [TestClass]
    public class FileDatabaseTests
    {
        public FileDatabaseTests()
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
        public void Minus_SameDatabase()
        {
            var database1 = GetSampleDatabase1();
            var database2 = GetSampleDatabase1();

            var database1Minus2 = database1.Minus(database2);
            Assert.AreEqual(0, database1Minus2.Count);
        }

        [TestMethod]
        public void Minus_Database2Minus1()
        {
            var database1 = GetSampleDatabase1();
            var database2 = GetSampleDatabase2();

            var database2Minus1 = database2.Minus(database1);
            Assert.AreEqual(3, database2Minus1.Count);
            Assert.IsTrue(database2.ContainsPath(@"a\f\bar.txt"));
            Assert.IsTrue(database2.ContainsPath(@"a\f\spam.txt"));
            Assert.IsTrue(database2.ContainsPath(@"a\h\eggs.txt"));
        }

        [TestMethod]
        public void Minus_Database1Minus2()
        {
            var database1 = GetSampleDatabase1();
            var database2 = GetSampleDatabase2();

            var database1Minus2 = database1.Minus(database2);
            Assert.AreEqual(0, database1Minus2.Count);
        }

        [TestMethod]
        public void Minus_Database1Minus3()
        {
            var database1 = GetSampleDatabase1();
            var database3 = GetSampleDatabase3();

            var database1Minus3 = database1.Minus(database3);
            Assert.AreEqual(database1.Count, database1Minus3.Count);
        }

        private FileDatabase GetSampleDatabase1()
        {
            List<FileDatabaseEntry> sampleEntries = GetSampleEntries1();
            return new FileDatabase(sampleEntries);
        }

        private FileDatabase GetSampleDatabase2()
        {
            List<FileDatabaseEntry> sampleEntries = GetSampleEntries1Subset();
            return new FileDatabase(sampleEntries);
        }

        private FileDatabase GetSampleDatabase3()
        {
            List<FileDatabaseEntry> sampleEntries = GetSampleEntries3();
            return new FileDatabase(sampleEntries);
        }

        private static List<FileDatabaseEntry> GetSampleEntries1Subset()
        {
            var entries = GetSampleEntries1();
            entries.AddRange(GetSampleEntries3());
            return entries;
        }

        private static List<FileDatabaseEntry> GetSampleEntries3()
        {
            var entries = new List<FileDatabaseEntry>();
            entries.Add(new FileDatabaseEntry()
            {
                LocalPath = @"F:\Foo\a\f\bar.txt",
                VirtualPath = @"a\f\bar.txt",
                LastModificationDate = new DateTime(2016, 3, 22),
                Size = 1024 * 778
            });

            entries.Add(new FileDatabaseEntry()
            {
                LocalPath = @"F:\Foo\a\f\spam.txt",
                VirtualPath = @"a\f\spam.txt",
                LastModificationDate = new DateTime(2016, 3, 23),
                Size = 1024 * 100
            });

            entries.Add(new FileDatabaseEntry()
            {
                LocalPath = @"F:\Foo\a\h\eggs.txt",
                VirtualPath = @"a\h\eggs.txt",
                LastModificationDate = new DateTime(2017, 6, 11),
                Size = 1024 * 1024 * 1024
            });
            return entries;
        }

        private static List<FileDatabaseEntry> GetSampleEntries1()
        {
            return new List<FileDatabaseEntry>()
            {
                new FileDatabaseEntry()
                {
                    LocalPath = @"C:\Foo\a\b\bar.txt",
                    VirtualPath = @"a\b\bar.txt",
                    LastModificationDate = new DateTime(2016, 12, 3),
                    Size = 1024 * 512
                },
                new FileDatabaseEntry()
                {
                    LocalPath = @"C:\Foo\a\b\spam.txt",
                    VirtualPath = @"a\b\spam.txt",
                    LastModificationDate = new DateTime(2017, 1, 17),
                    Size = 1024 * 1024 * 9
                },
                new FileDatabaseEntry()
                {
                    LocalPath = @"C:\Foo\a\b\c\bar.txt",
                    VirtualPath = @"a\b\c\bar.txt",
                    LastModificationDate = new DateTime(2017, 2, 5),
                    Size = 1024 * 1024 * 21 + 1024 * 24
                },
                new FileDatabaseEntry()
                {
                    LocalPath = @"C:\Foo\a\d\bar.txt",
                    VirtualPath = @"a\d\bar.txt",
                    LastModificationDate = new DateTime(2016, 12, 21),
                    Size = 1024 * 674
                },
                new FileDatabaseEntry()
                {
                    LocalPath = @"C:\Foo\a\eggs.txt",
                    VirtualPath = @"a\eggs.txt",
                    LastModificationDate = new DateTime(2017, 2, 22),
                    Size = 1024 * 1024 * 2
                }
            };
        }
    }
}
