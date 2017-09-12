using SeranfuenMirrorSync.ViewModels;
using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSync.Converters
{
    public static class SyncScheduleDataViewModelConverter
    {
        public static ISchedule ToScheduleDataModel(this SyncScheduleViewModel viewModel)
        {
            if (viewModel.Manual)
            {
                return GetManualSyncSchedule(viewModel);
            } else
            {
                return GetWeekdaySyncSchedule(viewModel);
            }
        }

        private static ISchedule GetWeekdaySyncSchedule(this SyncScheduleViewModel viewModel)
        {
            return new WeekdaySchedule(GetDayOfWeekFlag(viewModel), new Time(viewModel.Hour), GetSourcePaths(viewModel), viewModel.MirrorFolder)
            {
                Name = viewModel.SyncName
            };
        }

        private static DaysOfWeekFlag GetDayOfWeekFlag(SyncScheduleViewModel viewModel)
        {
            if (viewModel.Everyday)
            {
                return DaysOfWeekFlag.EveryDay;
            }
            else
            {
                var flag = 0;
                if (viewModel.Monday) flag |= 0x1;
                if (viewModel.Tuesday) flag |= 0x2;
                if (viewModel.Wednesday) flag |= 0x4;
                if (viewModel.Thursday) flag |= 0x8;
                if (viewModel.Friday) flag |= 0x10;
                if (viewModel.Saturday) flag |= 0x20;
                if (viewModel.Sunday) flag |= 0x40;

                return (DaysOfWeekFlag)flag;
            }
        }

        private static ISchedule GetManualSyncSchedule(SyncScheduleViewModel viewModel)
        {
            return new ManualSchedule(GetSourcePaths(viewModel), viewModel.MirrorFolder)
            {
                Name = viewModel.SyncName
            };
        }

        private static List<string> GetSourcePaths(SyncScheduleViewModel viewModel)
        {
            return viewModel.SyncSourcesViewModel.ListItems.Select(path => path.Path).ToList();
        }

        public static SyncScheduleViewModel ToSyncScheduleViewModel(this ISchedule schedule)
        {
            var viewModel = InitializeViewModelData(schedule);
            if (schedule is ManualSchedule)
            {
                viewModel.Manual = true;
            }
            else if (schedule is WeekdaySchedule)
            {
                var weekdaySchedule = (WeekdaySchedule)schedule;
                viewModel.Manual = false;
                viewModel.Monday = (weekdaySchedule.DaysOfWeekFlag & DaysOfWeekFlag.Monday) > 0;
                viewModel.Tuesday = (weekdaySchedule.DaysOfWeekFlag & DaysOfWeekFlag.Tuesday) > 0;
                viewModel.Wednesday = (weekdaySchedule.DaysOfWeekFlag & DaysOfWeekFlag.Wednesday) > 0;
                viewModel.Thursday = (weekdaySchedule.DaysOfWeekFlag & DaysOfWeekFlag.Thursday) > 0;
                viewModel.Friday = (weekdaySchedule.DaysOfWeekFlag & DaysOfWeekFlag.Friday) > 0;
                viewModel.Saturday = (weekdaySchedule.DaysOfWeekFlag & DaysOfWeekFlag.Saturday) > 0;
                viewModel.Sunday = (weekdaySchedule.DaysOfWeekFlag & DaysOfWeekFlag.Sunday) > 0;

                viewModel.Hour = weekdaySchedule.Time.ToDateTime();
            }
            else
            {
                throw new ArgumentException(string.Format("Unknown implementation of ISchedule. Type given: '{0}'", schedule.GetType().Name));
            }
            return viewModel;
        }

        private static SyncScheduleViewModel InitializeViewModelData(ISchedule schedule)
        {
            var viewModel = new SyncScheduleViewModel();
            viewModel.SyncSourcesViewModel = new SyncSourcesViewModel()
            {
                ListItems = new System.Collections.ObjectModel.ObservableCollection<PathViewModel>(schedule.SourcePaths.Select(path => new PathViewModel(path)))
            };
            viewModel.SyncName = schedule.Name;
            viewModel.MirrorFolder = schedule.DestinationPath;
            return viewModel;
        }
    }
}