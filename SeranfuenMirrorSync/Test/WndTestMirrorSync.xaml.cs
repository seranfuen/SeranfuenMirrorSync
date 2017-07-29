using SeranfuenMirrorSync.Test.ViewModels;
using SeranfuenMirrorSyncLib.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SeranfuenMirrorSync.Test
{
    /// <summary>
    /// Lógica de interacción para WndTestMirrorSync.xaml
    /// </summary>
    public partial class WndTestMirrorSync : Window
    {
        private bool _working = false;
        private SourceMirrorSynchronizationController _controller;
        private Timer _timer;

        public WndTestMirrorSync()
        {
            InitializeComponent();
            ViewModel = new TestMirrorSyncViewModel();
        }

        private TestMirrorSyncViewModel ViewModel
        {
            get
            {
                return DataContext as TestMirrorSyncViewModel;
            }
            set
            {
                DataContext = value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CmdRun_Click(object sender, RoutedEventArgs e)
        {
            if (!SourceTextBox.HasSelectedPath || !MirrorTextBox.HasSelectedPath)
            {
                MessageBox.Show("The source or mirror paths have not been selected", "No paths", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                CmdClose.IsEnabled = false;
                CmdRun.IsEnabled = false;
                _working = true;

                var sourcePath = SourceTextBox.SelectedPath;
                var mirrorPath = MirrorTextBox.SelectedPath;

                Task.Factory.StartNew(() =>
                {
                    _controller = new SourceMirrorSynchronizationController(sourcePath, mirrorPath);
                    _timer = new Timer(StatusTimerTicked, null, 500, Timeout.Infinite);
                    _controller.RunSynchronization();
                    _timer.Dispose();
                    _working = false;
                }).ContinueWith((task) =>
                {
                    if (task.IsFaulted)
                    {
                        MessageBox.Show(string.Format("The process failed with exception\n\n'{0}'", task.Exception.InnerException.Message), "Sync failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    lock (this)
                    {
                        _working = false;
                    }

                    Dispatcher.Invoke(() =>
                    {
                        CmdClose.IsEnabled = true;
                        CmdRun.IsEnabled = true;
                        var status = _controller.GetStatus();
                        ViewModel.Status = string.Format("The sync finished in {0} s", (status.End.Value - status.Start).TotalSeconds);
                    });
                }); ;
            }
        }

        private void StatusTimerTicked(object state)
        {
            var status = _controller.GetStatus();
            Dispatcher.BeginInvoke((Action)(() =>
            {
                ViewModel.FileSyncActions = status.FileSyncActionStatuses;
                ViewModel.Status = string.Format("Running {0} threads for {1} s", status.Threads, (DateTime.Now - status.Start).TotalSeconds);
            }));
            lock (this)
            {
                if (_working)
                {
                    _timer.Change(500, Timeout.Infinite);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !_working;
        }
    }
}
