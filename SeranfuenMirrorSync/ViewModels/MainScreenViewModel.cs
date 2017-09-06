using SeranfuenMirrorSync.StringResources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeranfuenMirrorSync.ViewModels
{
    public class MainScreenViewModel : ViewModelList<SyncScheduleViewModel>
    {
        #region ' Events '

        public event EventHandler<RunCurrentSyncEventArgs> RunCurrentSync;

        #endregion

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
                _parent.OnRunCurrentSync();
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
            get;
            set;
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

        protected virtual void OnRunCurrentSync()
        {
            RunCurrentSync?.Invoke(this, new RunCurrentSyncEventArgs(Current));
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

    internal class AssemblyVersionInfo
    {
    }

    #region ' RunCurrentSyncEventArgs '

    public class RunCurrentSyncEventArgs : EventArgs
    {
        #region ' Constructor '

        public RunCurrentSyncEventArgs(SyncScheduleViewModel syncToRun)
        {
            SyncToRun = syncToRun;
        }

        #endregion

        #region ' Properties '

        public SyncScheduleViewModel SyncToRun
        {
            get;
            private set;
        }

        #endregion
    }

    #endregion
}
