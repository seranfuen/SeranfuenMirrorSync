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
using SeranfuenMirrorSync.Converters;
using System.Threading;

namespace SeranfuenMirrorSync.Windows
{
    /// <summary>
    /// Interaction logic for WndMainScreen.xaml
    /// </summary>
    public partial class WndMainScreen : Window
    {
        #region ' Fields '

        private Timer _timer;
        private const int TIMER_PERIOD_MS = 1000;

        #endregion

        #region ' Ctor '

        public WndMainScreen()
        {
            InitializeComponent();
            Loaded += WndMainScreen_Loaded;
            _timer = new Timer(Timer_Callback, null, TIMER_PERIOD_MS, TIMER_PERIOD_MS);
        }

        #endregion

        #region ' Properties '

        private MainScreenViewModel ViewModel
        {
            get
            {
                return DataContext as MainScreenViewModel;
            }
        }

        #endregion

        #region ' Members '

        private void WndMainScreen_Loaded(object sender, RoutedEventArgs e)
        {
            var dataContext = (MainScreenViewModel)DataContext;
            dataContext.ShowMessageRequested += DataContext_ShowMessageRequested;
            dataContext.RequestedClose += DataContext_RequestedClose;
            dataContext.LoadData();
        }

        private void DataContext_RequestedClose(object sender, RequestedConfirmationEventArgs e)
        {
            Close();
        }

        private void DataContext_ShowMessageRequested(object sender, ShowMessageRequestedEventArgs e)
        {
            MessageBox.Show(e.Message, e.Title, MessageBoxButton.OK, e.Type.FromViewModelMessageType());
        }

        private void Timer_Callback(object state)
        {
            Dispatcher.Invoke(() =>
            {
                if (ViewModel != null)
                {
                    ViewModel.UpdateLastStatus();
                    ViewModel.RefreshCurrentHistory();
                }
            });
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel != null && ViewModel.Current != null)
            {
                ShowSyncManager();
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel != null && ViewModel.Current != null;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowSyncManager();
        }

        private void ShowSyncManager()
        {
            var wnd = new WndSyncScheduleManager();
            wnd.ShowDialog();
            ViewModel.LoadData();
        }

        #endregion

    }
}
