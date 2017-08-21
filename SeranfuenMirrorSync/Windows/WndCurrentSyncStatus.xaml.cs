using SeranfuenMirrorSyncLib.Data;
using SeranfuenMirrorSyncWcfClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace SeranfuenMirrorSync.Windows
{
    /// <summary>
    /// Interaction logic for WndCurrentSyncStatus.xaml
    /// </summary>
    public partial class WndCurrentSyncStatus : Window
    {
        private DispatcherTimer _timer;
        private bool _isRunning;

        public WndCurrentSyncStatus()
        {
            InitializeComponent();
        }

        #region ' Methods '

        public void StartUpdating()
        {
            _isRunning = true;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 1);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            var worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // post update error
            }
            else
            {
                var status = (SourceMirrorSyncStatus)e.Result;
                DataContext = status;
            }
            if (_isRunning)
            {
                _timer.Start();
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var client = SeranfuenMirrorSyncWcfClient.ServiceProxyFactory.Proxy;
            var result = client.GetCurrentSyncStatus(true);
            e.Result = result;
        }

        public void StopUpdating()
        {
            _isRunning = false;
            _timer.Stop();
        }

        private void CmdCancelSync_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DataContext != null && !((SourceMirrorSyncStatus)DataContext).HasFinished;
        }

        private void CmdCancelSync_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                var proxy = ServiceProxyFactory.Proxy;
                proxy.CancelCurrentSync();
            } catch (Exception ex)
            {
                // show error
            }
        }

        private void CmdCloseWindow_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CmdCloseWindow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}
