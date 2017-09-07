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

namespace SeranfuenMirrorSync.Windows
{
    /// <summary>
    /// Interaction logic for WndMainScreen.xaml
    /// </summary>
    public partial class WndMainScreen : Window
    {
        #region ' Ctor '

        public WndMainScreen()
        {
            InitializeComponent();
            Loaded += WndMainScreen_Loaded;
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

        #endregion
    }
}
