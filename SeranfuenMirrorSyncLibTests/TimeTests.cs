using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeranfuenMirrorSyncLib.Controllers;

namespace SeranfuenMirrorSyncLibTests
{
    /// <summary>
    /// Summary description for TimeTests
    /// </summary>
    [TestClass]
    public class TimeTests
    {
        public TimeTests()
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
        public void TimeOK_23_59()
        {
            var time = new Time(23, 59);
            Assert.AreEqual(23, time.Hour);
            Assert.AreEqual(59, time.Minute);
        }

        [TestMethod]
        public void TimeOK_00_00()
        {
            var time = new Time(0, 0);
            Assert.AreEqual(0, time.Hour);
            Assert.AreEqual(0, time.Minute);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TimeNotOK_24_00()
        {
            var time = new Time(24, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TimeNotOK_23_60()
        {
            var time = new Time(23, 60);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TimeNotOK_Minus5_0()
        {
            var time = new Time(-5, 0);
        }
    }
}
