using SeranfuenMirrorSync.ViewModels;
using SeranfuenMirrorSyncWcfClient;
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

namespace SeranfuenMirrorSync.Windows
{
    /// <summary>
    /// Interaction logic for WndManualSync.xaml
    /// </summary>
    public partial class WndManualSync : Window
    {
        public WndManualSync()
        {
            InitializeComponent();
            var viewModel = new ManualSyncViewModel();
            viewModel.UseSourceFolderName = true;
            DataContext = viewModel;
        }

        public ManualSyncViewModel ViewModel
        {
            get
            {
                return DataContext as ManualSyncViewModel;
            }
        }

        private void Sync_Click(object sender, RoutedEventArgs e)
        {
            var proxy = ServiceProxyFactory.Proxy;
            proxy.RunSync(ViewModel.SourcePath, ViewModel.FinalMirrorPath);
        }
    }
}
