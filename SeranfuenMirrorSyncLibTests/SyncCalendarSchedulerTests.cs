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
            var scheduler = new SyncCalendarScheduler();
            scheduler.TimeProvider = new StaticTimeProvider(new DateTime(2017, 8, 4, 20, 25, 0)); // thursday
            var schedule = new WeekdaySchedule(DayOfWeek.Monday | DayOfWeek.Thursday, new Time(20, 30));
            scheduler.PutSchedule(schedule);
            var isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsFalse(isScheduled);
        }

        [TestMethod]
        public void CheckScheduleAfterInRange_Scheduled()
        {
            var scheduler = new SyncCalendarScheduler();
            scheduler.TimeProvider = new StaticTimeProvider(new DateTime(2017, 8, 4, 20, 31, 0)); // thursday
            var schedule = new WeekdaySchedule(DayOfWeek.Monday | DayOfWeek.Thursday, new Time(20, 30));
            scheduler.PutSchedule(schedule);
            var isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsTrue(isScheduled);
        }

        [TestMethod]
        public void CheckScheduleAfterNotInRange_NotScheduled()
        {
            var scheduler = new SyncCalendarScheduler();
            scheduler.TimeProvider = new StaticTimeProvider(new DateTime(2017, 8, 4, 20, 31, 0)); // thursday
            var schedule = new WeekdaySchedule(DayOfWeek.Monday | DayOfWeek.Thursday, new Time(21, 35));
            scheduler.PutSchedule(schedule);
            var isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsTrue(isScheduled);
        }

        [TestMethod]
        public void CheckScheduleNotifyDone_NotScheduled()
        {
            var scheduler = new SyncCalendarScheduler();
            scheduler.TimeProvider = new StaticTimeProvider(new DateTime(2017, 8, 4, 20, 31, 0)); // thursday
            var schedule = new WeekdaySchedule(DayOfWeek.Monday | DayOfWeek.Thursday, new Time(20, 30));
            scheduler.PutSchedule(schedule);
            var isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsTrue(isScheduled);

            scheduler.NotifyDone(schedule);
            isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsFalse(isScheduled);
        }

        [TestMethod]
        public void CheckWeeklySchedule()
        {
            var scheduler = new SyncCalendarScheduler();
            var timeProvider = new IncreasingDayProvider(new DateTime(2017, 7, 31, 20, 30, 0)); // starts on monday;
            scheduler.TimeProvider = timeProvider;
            var schedule = new WeekdaySchedule(DayOfWeek.Monday | DayOfWeek.Thursday, new Time(20, 30));
            scheduler.PutSchedule(schedule);
            var isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsTrue(isScheduled);
            scheduler.NotifyDone(schedule);

            timeProvider.Step();
            isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsFalse(isScheduled); // tuesday

            timeProvider.Step();
            isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsFalse(isScheduled); // 

            timeProvider.Step();
            isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsTrue(isScheduled); // Thursday
            scheduler.NotifyDone(schedule);

            timeProvider.Step();
            isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsFalse(isScheduled); // Friday

            timeProvider.Step();
            isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsFalse(isScheduled); // Saturday

            timeProvider.Step();
            isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsFalse(isScheduled); // Sunday

            timeProvider.Step();
            isScheduled = scheduler.CheckScheduled(schedule);
            Assert.IsTrue(isScheduled); // Monday
        }
    }
}
