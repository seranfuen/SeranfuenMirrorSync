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
    /// Interaction logic for TestMainForm.xaml
    /// </summary>
    public partial class WndTestMainForm : Window
    {
        private bool bIsWorking = false;

        public WndTestMainForm()
        {
            InitializeComponent();
        }

        private void CmdRandomFolders_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !bIsWorking;
        }

        private void CmdRandomFolders_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            bIsWorking = true;
            var form = new WndRandomFolderGenerator();
            form.Show();
            form.Closed += (s, ex) => bIsWorking = false;
        }

        private void CmdTestFileCrawler_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            bIsWorking = true;
            var form = new WndTestFileDatabaseCrawl();
            form.Show();
            form.Closed += (s, ex) => bIsWorking = false;
        }

        private void CmdSyncFolderStructure_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            bIsWorking = true;
            var form = new WndTestSyncFolders();
            form.Show();
            form.Closed += (s, ex) => bIsWorking = false;
        }
    }
}
