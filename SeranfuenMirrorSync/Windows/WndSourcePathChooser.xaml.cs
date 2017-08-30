using SeranfuenMirrorSync.Converters;
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

namespace SeranfuenMirrorSync.Windows
{
    /// <summary>
    /// Lógica de interacción para 
    /// SourcePathChooser.xaml
    /// </summary>
    public partial class WndSourcePathChooser : Window
    {
        public WndSourcePathChooser()
        {
            InitializeComponent();
        }

        public void SetSourcePathChooserViewModel(SyncSourcesViewModel viewModel)
        {
            var context = DataContext as AddSyncSourceViewModel;
            context.SyncSourcesViewModel = viewModel;
            viewModel.ShowMessageRequested += ViewModel_ShowMessageRequested;
            viewModel.UserConfirmationRequested += ViewModel_UserConfirmationRequested;
            viewModel.RequestedClose += ViewModel_RequestedClose;
        }

        private void ViewModel_RequestedClose(object sender, RequestedConfirmationEventArgs e)
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            var context = DataContext as AddSyncSourceViewModel;
            var viewModel = context.SyncSourcesViewModel;
            viewModel.ShowMessageRequested -= ViewModel_ShowMessageRequested;
            viewModel.UserConfirmationRequested -= ViewModel_UserConfirmationRequested;
            viewModel.RequestedClose -= ViewModel_RequestedClose;
            base.OnClosed(e);
        }

        private void ViewModel_UserConfirmationRequested(object sender, UserConfirmationRequestedEventArgs e)
        {
            e.Cancel = MessageBox.Show(e.Message, e.Title, MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes;
        }

        private void ViewModel_ShowMessageRequested(object sender, ShowMessageRequestedEventArgs e)
        {
            MessageBox.Show(e.Message, e.Title, MessageBoxButton.OK, MessageBoxImageConverter.FromViewModelMessageType(e.Type));
        }
    }
}
