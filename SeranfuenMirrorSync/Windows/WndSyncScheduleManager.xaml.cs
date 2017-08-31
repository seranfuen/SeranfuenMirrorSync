﻿using SeranfuenMirrorSync.Converters;
using SeranfuenMirrorSync.StringResources;
using SeranfuenMirrorSync.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace SeranfuenMirrorSync.Windows
{
    /// <summary>
    /// Interaction logic for WndSyncScheduleManager.xaml
    /// </summary>
    public partial class WndSyncScheduleManager : Window
    {
        public WndSyncScheduleManager()
        {
            InitializeComponent();
            var viewModel = new SyncScheduleManagerViewModel();
            viewModel.RequestedSave += ViewModel_RequestedSave;
            viewModel.RequestedClose += ViewModel_RequestedClose;
            DataContext = viewModel;
            
        }

        #region ' Properties '

        private SyncScheduleManagerViewModel Current
        {
            get
            {
                return DataContext as SyncScheduleManagerViewModel;
            }
        }

        #endregion

        #region ' Memebers '

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Current != null && Current.Current != null;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var parameter = e.Parameter as SyncSourcesViewModel;
            if (parameter != null)
            {
                var window = new WndSourcePathChooser();
                window.SetSourcePathChooserViewModel(parameter);
                window.ShowDialog();
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Current.Current != null)
            {
                var currentSourceViewModel = Current.Current.SyncSourcesViewModel;
                if (currentSourceViewModel.Current != null)
                {
                    var dataModel = new AddSyncSourceViewModel()
                    {
                        SourcePath = currentSourceViewModel.Current.Path,
                        SyncSourcesViewModel = currentSourceViewModel
                    };
                    var formAdd = new WndSourcePathChooser();
                    formAdd.DataContext = dataModel;
                    formAdd.SetSourcePathChooserViewModel(currentSourceViewModel);
                    formAdd.ShowDialog();
                }
            }
        }

        private void ViewModel_RequestedClose(object sender, RequestedConfirmationEventArgs e)
        {
            Close();
        }

        private void ViewModel_RequestedSave(object sender, ConfirmSaveAndCloseEventArgs e)
        {
            var result = MessageBox.Show(AppStrings.ConfirmSave_Message, AppStrings.ConfirmSave_Title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            e.ConfirmSave = result.ToBool();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var viewModel = DataContext as SyncScheduleManagerViewModel;
            e.Cancel = viewModel != null;
            base.OnClosing(e);
            if (e.Cancel)
            {
                viewModel.SaveCloseCommand.Execute(null);
            }
        }

        #endregion
    }
}