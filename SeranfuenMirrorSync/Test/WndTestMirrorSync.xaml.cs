using SeranfuenMirrorSync.Test.ViewModels;
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
    /// Lógica de interacción para WndTestMirrorSync.xaml
    /// </summary>
    public partial class WndTestMirrorSync : Window
    {
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

        }
    }
}
