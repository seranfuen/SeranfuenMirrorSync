using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncLib.Data;

namespace SeranfuenMirrorSyncLibTests
{
    /// <summary>
    /// Summary description for SyncCalendarSchedulerTests
    /// </summary>
    [TestClass]
    public class SyncCalendarSchedulerTests
    {
        public SyncCalendarSchedulerTests()
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
        public void CheckScheduleBefore_NotScheduled()
        {
            var schedule = new WeekdaySchedule(DaysOfWeekFlag.Monday | DaysOfWeekFlag.Thursday, new Time(20, 30), null, null);
            schedule.TimeProvider = new StaticTimeProvider(new DateTime(2017, 8, 3, 20, 25, 0)); // thursday
            var isScheduled = schedule.IsScheduled;
            Assert.IsFalse(isScheduled);
        }

        [TestMethod]
        public void CheckScheduleAfterInRange_Scheduled()
        {
            var schedule = new WeekdaySchedule(DaysOfWeekFlag.Monday | DaysOfWeekFlag.Thursday, new Time(20, 30), null, null);
            schedule.TimeProvider = new StaticTimeProvider(new DateTime(2017, 8, 3, 20, 31, 0)); // thursday
            var isScheduled = schedule.IsScheduled;
            Assert.IsTrue(isScheduled);
        }

        [TestMethod]
        public void CheckScheduleAfterNotInRange_NotScheduled()
        {
            var schedule = new WeekdaySchedule(DaysOfWeekFlag.Monday | DaysOfWeekFlag.Thursday, new Time(21, 35), null, null);
            schedule.TimeProvider = new StaticTimeProvider(new DateTime(2017, 8, 3, 20, 31, 0)); // thursday
            var isScheduled = schedule.IsScheduled;
            Assert.IsFalse(isScheduled);
        }

        [TestMethod]
        public void CheckScheduleNotifyDone_NotScheduled()
        {
            var schedule = new WeekdaySchedule(DaysOfWeekFlag.Monday | DaysOfWeekFlag.Thursday, new Time(20, 30), null, null);
            schedule.TimeProvider = new StaticTimeProvider(new DateTime(2017, 8, 3, 20, 31, 0)); // thursday
            var isScheduled = schedule.IsScheduled;
            Assert.IsTrue(isScheduled);

            schedule.SetSyncRun();
            isScheduled = schedule.IsScheduled;
            Assert.IsFalse(isScheduled);
        }

        [TestMethod]
        public void CheckWeeklySchedule()
        {
            var schedule = new WeekdaySchedule(DaysOfWeekFlag.Monday | DaysOfWeekFlag.Thursday, new Time(20, 30), null, null);
            var timeProvider = new IncreasingDayProvider(new DateTime(2017, 7, 31, 20, 30, 0)); // starts on monday;
            schedule.TimeProvider = timeProvider;

            var isScheduled = schedule.IsScheduled;
            Assert.IsTrue(isScheduled);
            schedule.SetSyncRun();

            timeProvider.Step();
            isScheduled = schedule.IsScheduled;
            Assert.IsFalse(isScheduled); // tuesday

            timeProvider.Step();
            isScheduled = schedule.IsScheduled;
            Assert.IsFalse(isScheduled); // 

            timeProvider.Step();
            isScheduled = schedule.IsScheduled;
            Assert.IsTrue(isScheduled); // Thursday
            

            timeProvider.Step();
            isScheduled = schedule.IsScheduled;
            Assert.IsFalse(isScheduled); // Friday

            timeProvider.Step();
            isScheduled = schedule.IsScheduled;
            Assert.IsFalse(isScheduled); // Saturday

            timeProvider.Step();
            isScheduled = schedule.IsScheduled;
            Assert.IsFalse(isScheduled); // Sunday

            timeProvider.Step();
            isScheduled = schedule.IsScheduled;
            Assert.IsTrue(isScheduled); // Monday
        }
    }
}
