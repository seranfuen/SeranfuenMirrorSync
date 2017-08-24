using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeranfuenMirrorSyncLib.Controllers;


namespace SeranfuenMirrorSyncLibTests
{
    /// <summary>
    /// Summary description for ExtensionMethodsTests
    /// </summary>
    [TestClass]
    public class ExtensionMethodsTests
    {
        public ExtensionMethodsTests()
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void Capitalize_Null()
        {
            string str = null;
            str.Capitalize();
        }

        [TestMethod]
        public void Capitalize_Empty()
        {
            string str = string.Empty;
            var result = str.Capitalize();
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Capitalize_Numbers()
        {
            string str = "123";
            var result = str.Capitalize();
            Assert.AreEqual(str, result);
        }

        [TestMethod]
        public void Capitalize_OneLetter()
        {
            string str = "a";
            var result = str.Capitalize();
            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void Capitalize_Pharse()
        {
            string str = "foobar";
            var result = str.Capitalize();
            Assert.AreEqual("Foobar", result);
        }
    }
}
