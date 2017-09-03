using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeranfuenMirrorSync.ViewModels;
using System.Collections.Generic;
using SeranfuenMirrorSync.Converters;

namespace SeranfuenMirrorSyncLibTests
{
    [TestClass]
    public class SyncScheduleDataViewModelConverterTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FromViewModel_Data_NoWeekdaySelected()
        {
            var viewModel = InitializeViewModel();
            var result = viewModel.ToScheduleDataModel();
        }

        [TestMethod]
        public void FromViewModel_Data_Everyday()
        {
            var viewModel = InitializeViewModel();
            viewModel.Everyday = true;
            var syncSchedule = viewModel.ToScheduleDataModel();
            var startingTest = new DateTime(2017, 9, 4, 22, 01, 0); // Monday
            var timeProvider = new IncreasingDayProvider(startingTest);
            syncSchedule.TimeProvider = timeProvider;
            for (int i = 0; i < 7; i++)
            { // test a whole week
                Assert.IsTrue(syncSchedule.IsScheduled);
                timeProvider.Step();
            }
        }

        [TestMethod]
        public void FromViewModel_Data_MondayTuesdayWednesdaySunday()
        {
            var viewModel = InitializeViewModel();
            viewModel.Monday = true;
            viewModel.Tuesday = true;
            viewModel.Wednesday = true;
            viewModel.Sunday = true;
            var syncSchedule = viewModel.ToScheduleDataModel();
            var startingTest = new DateTime(2017, 9, 4, 22, 01, 0); // Monday
            var timeProvider = new IncreasingDayProvider(startingTest);
            syncSchedule.TimeProvider = timeProvider;
            for (int i = 0; i < 7; i++)
            { // test a whole week
                if (i >= 3 && i != 6)
                {
                    Assert.IsFalse(syncSchedule.IsScheduled);
                }
                else
                {
                    Assert.IsTrue(syncSchedule.IsScheduled);
                }
                timeProvider.Step();
            }
        }

        private static SyncScheduleViewModel InitializeViewModel()
        {
            var viewModel = new SyncScheduleViewModel();
            viewModel.SyncSourcesViewModel = new SyncSourcesViewModel()
            {
                ListItems = new System.Collections.ObjectModel.ObservableCollection<PathViewModel>(
                    new List<PathViewModel>() {
                    new PathViewModel(@"C:\Foo"),
                    new PathViewModel(@"C:\Bar")
                    })
            };
            viewModel.SyncName = "SYNC #1";
            viewModel.MirrorFolder = @"D:\Spam\Eggs";
            viewModel.Hour = new DateTime(2017, 9, 4, 22, 00, 0);
            return viewModel;
        }
    }
}
