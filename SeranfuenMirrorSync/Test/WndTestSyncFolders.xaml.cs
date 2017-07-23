using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncLib.Data;
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

namespace SeranfuenMirrorSync.Test
{
    /// <summary>
    /// Interaction logic for WndTestSyncFolders.xaml
    /// </summary>
    public partial class WndTestSyncFolders : Window
    {
        private const string SYNC_TEXT = "Synchronizing folders";
        private bool _isWorking = false;
        private List<FolderSyncAction> _listSyncActions;

        class FolderSyncProgress
        {
            public int FoldersSynced;
            public int FoldersDeletedMirror;
            public int FoldersAddedMirror;
            public int FoldersSkipped;
            public int FoldersFailed;
            public DateTime DateStart;
            public DateTime DateEnd;
        }

        private FolderSyncProgress _progress;

        public WndTestSyncFolders()
        {
            InitializeComponent();
        }

        private void CmdTestSyncFolders_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !_isWorking;
        }

        private void CmdTestSyncFolders_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _isWorking = true;
            ProgressBar.Visibility = Visibility.Visible;
            StatusLabel.Content = SYNC_TEXT;
            _progress = new FolderSyncProgress();
            _progress.DateStart = DateTime.Now;
            _listSyncActions = new List<FolderSyncAction>();

            var sourceRootPath = SourceTextBox.Text;
            var mirrorRootPath = MirrorTextBox.Text;

            Task.Factory.StartNew(() =>
            {
                var controller = new FolderSyncController(sourceRootPath, mirrorRootPath);
                controller.FolderSynced += Controller_FolderSynced;
                controller.StartSync();
            }).ContinueWith((task) =>
            {
                _isWorking = false;
                _progress.DateEnd = DateTime.Now;
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    var sb = new StringBuilder();
                    if (task.IsFaulted)
                    {
                        sb.Append("The task finished with errors. ");
                    }
                    else
                    {
                        sb.Append("The task finished successfully. ");
                    }
                    sb.Append(string.Format("Completed in {0} ms. ", (_progress.DateEnd - _progress.DateStart).TotalMilliseconds));
                    sb.Append(string.Format("{0} folders processed, {1} created, {2} deleted, {3} failed", _progress.FoldersSynced, _progress.FoldersAddedMirror, _progress.FoldersDeletedMirror, _progress.FoldersFailed));
                    StatusLabel.Content = sb.ToString();
                    ActionsGrid.ItemsSource = _listSyncActions;
                    ProgressBar.Visibility = Visibility.Hidden;
                }));
            });
        }

        private void Controller_FolderSynced(object sender, FolderSyncEventArgs e)
        {
            lock (this)
            {
                _listSyncActions.Add(e.SyncAction);
                if (!e.SyncAction.Failed)
                {
                    _progress.FoldersSynced++;
                    if (e.SyncAction.Action == SeranfuenMirrorSyncLib.Data.FolderSyncAction.FolderAction.Create)
                    {
                        _progress.FoldersAddedMirror++;
                    }
                    else if (e.SyncAction.Action == SeranfuenMirrorSyncLib.Data.FolderSyncAction.FolderAction.Delete)
                    {
                        _progress.FoldersDeletedMirror++;
                    }
                    else
                    {
                        _progress.FoldersSkipped++;
                    }
                }
                else
                {
                    _progress.FoldersFailed++;
                }
            }
            Dispatcher.BeginInvoke((Action)(() =>
            {
                StatusLabel.Content = string.Format(SYNC_TEXT, e.SyncAction.Path);
            }));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
