using SeranfuenMirrorSync.StringResources;
using SeranfuenMirrorSyncWcfClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SeranfuenMirrorSync.Converters;
using System.ServiceModel;
using System.Windows.Threading;
using SeranfuenMirrorSyncLib.Data;
using SeranfuenMirrorSync.Properties;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;

namespace SeranfuenMirrorSync.ViewModels
{
    public class MainScreenViewModel : ViewModelList<SyncScheduleViewModel>
    {
        #region ' Command Definitions '

        private class RunCurrentSyncCommandDefinition : ICommand
        {
            #region ' Events '

            public event EventHandler CanExecuteChanged;

            #endregion

            #region ' Fields '

            private MainScreenViewModel _parent;

            #endregion

            #region ' Ctor '

            public RunCurrentSyncCommandDefinition(MainScreenViewModel parent)
            {
                _parent = parent;
                _parent.CurrentChanged += (s, e) => OnCanExecuteChanged();
            }

            #endregion

            #region ' Members '

            public bool CanExecute(object parameter)
            {
                return _parent.Current != null;
            }

            public void Execute(object parameter)
            {
                _parent.RunCurrentSync();
            }

            protected virtual void OnCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }

            #endregion
        }

        #endregion

        #region ' Fields '

        private bool _isConnected = false;
        private StatusViewModel _statusViewModel;
        private ObservableCollection<StatusViewModel> _selectedSyncHistory;

        #endregion

        #region ' Ctor '

        public MainScreenViewModel()
        {
            RunCurrentSyncCommand = new RunCurrentSyncCommandDefinition(this);
            CurrentChanged += MainScreenViewModel_CurrentChanged;
        }

        #endregion

        #region ' Properties '

        public override string DisplayName
        {
            get
            {
                return string.Format("{0} v.{1} - {2}", GetAssemblyName(), GetAssemblyVersion(), IsConnected ? AppStrings.ServiceRunning : AppStrings.NoServiceRunning);
            }
        }

        public StatusViewModel LastStatus
        {
            get
            {
                return _statusViewModel;
            }
            set
            {
                _statusViewModel = value;
                OnPropertyChanged("LastStatus");
            }
        }

        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
                OnPropertyChanged("IsConnected");
                OnPropertyChanged("DisplayName");
            }
        }

        public ObservableCollection<StatusViewModel> SelectedSyncStatusHistory
        {
            get
            {
                return _selectedSyncHistory;
            }
            set
            {
                _selectedSyncHistory = value;
                OnPropertyChanged("SelectedSyncStatusHistory");
            }
        }

        #endregion

        #region ' Commands '

        public ICommand CheckConnectionCommand
        {
            get;
            set;
        }

        public ICommand RunCurrentSyncCommand
        {
            get;
            set;
        }

        #endregion

        #region ' Members '

        public void UpdateLastStatus()
        {
            Task.Factory.StartNew<SourceMirrorSyncStatus>(() =>
            {
                var proxy = ServiceProxyFactory.Proxy;
                return proxy.GetCurrentSyncStatus(true);
            }).ContinueWith((taskResult) =>
            {
                if (!taskResult.IsFaulted && taskResult.Result != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        LastStatus = new StatusViewModel(taskResult.Result);
                    });
                }
                else if (taskResult.IsFaulted && taskResult.Exception.InnerException is CommunicationException)
                {
                    IsConnected = false;
                }
            });
        }

        public void LoadData()
        {
            try
            {
                var proxy = ServiceProxyFactory.Proxy;
                SetItems(proxy.GetSchedules().Select(schedule => schedule.ToSyncScheduleViewModel()));
                RefreshCurrentHistory();
                IsConnected = true;
            }
            catch (CommunicationException)
            {
                ShowCommunicationErrorMessage();
            }
        }

        public void RefreshCurrentHistory()
        {
            if (Current != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var proxy = ServiceProxyFactory.Proxy;
                    return proxy.GetHistorySyncStatus(Current.SyncName, Settings.Default.MaxStatusHistory);
                }).ContinueWith((taskResult) =>
                {
                    if (!taskResult.IsFaulted)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (taskResult.Result == null)
                            {
                                SelectedSyncStatusHistory = null;
                            }
                            else if (SelectedSyncStatusHistory == null)
                            {
                                SelectedSyncStatusHistory = new ObservableCollection<StatusViewModel>(taskResult.Result.Select(res => new StatusViewModel(res)).OrderByDescending(status => status.Start));
                            }
                            else
                            {
                                foreach (var status in taskResult.Result)
                                {
                                    if (!SelectedSyncStatusHistory.Any(stat => stat.Guid == status.Guid))
                                    {
                                        SelectedSyncStatusHistory.Add(new StatusViewModel(status));
                                    }
                                }
                            }
                        });
                    }
                    else if (taskResult.IsFaulted && taskResult.Exception.InnerException is CommunicationException)
                    {
                        IsConnected = false;
                    }
                });
            }
        }

        private void ShowCommunicationErrorMessage()
        {
            ShowUserMessage(AppStrings.NoService_Error, AppStrings.ErrorTitle, ShowMessageRequestedEventArgs.MessageType.Error);
            IsConnected = false;
        }

        private void MainScreenViewModel_CurrentChanged(object sender, EventArgs e)
        {
            RefreshCurrentHistory();
        }

        protected void RunCurrentSync()
        {
            if (Current != null)
            {
                var service = ServiceProxyFactory.Proxy;
                service.RunSyncs(Current.SyncName, Current.Sources, Current.MirrorFolder);
            }
        }

        private static string GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private static string GetAssemblyName()
        {
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute));
            return (attributes.SingleOrDefault() as AssemblyTitleAttribute).Title;
        }

        #endregion
    }
}
