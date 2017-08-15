using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeranfuenMirrorSyncLib.Data;
using SeranfuenMirrorSyncLib.Controllers;
using static SeranfuenMirrorSyncLib.Controllers.SyncProgressReportedEventArgs;

namespace SeranfuenMirrorSyncLibTests
{
    [TestClass]
    public class SyncSchedulerTests
    {
        [TestMethod]
        public void Test_Put3_Process()
        {
            var testController = new SyncSchedulerTestController();
            testController.RunThreeManualSyncsTest();
            Assert.AreEqual("a", testController.OrderedEventArgs[0].SourceRoot);
            Assert.AreEqual(SyncProgressReportType.SyncStart, testController.OrderedEventArgs[0].StatusType);
            Assert.AreEqual(SyncProgressReportType.SyncFinished, testController.OrderedEventArgs[1].StatusType);
            Assert.AreEqual("c", testController.OrderedEventArgs[2].SourceRoot);
            Assert.AreEqual(SyncProgressReportType.SyncStart, testController.OrderedEventArgs[2].StatusType);
            Assert.AreEqual(SyncProgressReportType.SyncFinished, testController.OrderedEventArgs[3].StatusType);
            Assert.AreEqual("e", testController.OrderedEventArgs[4].SourceRoot);
            Assert.AreEqual(SyncProgressReportType.SyncStart, testController.OrderedEventArgs[4].StatusType);
            Assert.AreEqual(SyncProgressReportType.SyncFinished, testController.OrderedEventArgs[5].StatusType);
        }
    }
}
