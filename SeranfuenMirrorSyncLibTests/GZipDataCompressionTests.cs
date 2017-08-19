using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeranfuenMirrorSyncLib.Controllers;
using System.Collections.Generic;

namespace SeranfuenMirrorSyncLibTests
{
    [TestClass]
    public class GZipDataCompressionTests
    {
        [Serializable]
        private class TestData
        {
            private int _test1;

            public int TestInteger
            {
                get { return _test1; }
                set { _test1 = value; }
            }

            public string TestString
            {
                get;
                set;
            }

            public void SquareInteger()
            {
                _test1 *= _test1;
            }
        }

        [TestMethod]
        public void CompressDecompress_Test()
        {
            var testData = new TestData();
            testData.TestInteger = 3;
            testData.SquareInteger();
            testData.TestString = "foo";

            var compressor = new GZipDataCompression<TestData>();
            var bytes = compressor.CompressObject(testData);

            var uncompressedData = compressor.DecompressObjecT(bytes);
            Assert.AreEqual(9, uncompressedData.TestInteger);
            Assert.AreEqual("foo", uncompressedData.TestString);
        }

        [TestMethod]
        public void CompressDecompressList_Test()
        {
            var listData = new List<TestData>();

            var testData1 = new TestData();
            testData1.TestInteger = 3;
            testData1.SquareInteger();
            testData1.TestString = "foo";

            var testData2 = new TestData();
            testData2.TestInteger = 5;
            testData2.SquareInteger();
            testData2.TestString = "bar";

            listData.Add(testData2);
            listData.Add(testData1);

            var compressor = new GZipDataCompression<List<TestData>>();
            var bytes = compressor.CompressObject(listData);

            var uncompressedData = compressor.DecompressObjecT(bytes);
            Assert.AreEqual(9, uncompressedData[1].TestInteger);
            Assert.AreEqual("foo", uncompressedData[1].TestString);

            Assert.AreEqual(25, uncompressedData[0].TestInteger);
            Assert.AreEqual("bar", uncompressedData[0].TestString);
        }
    }
}
