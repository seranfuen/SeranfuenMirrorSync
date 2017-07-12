using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SeranfuenMirrorSync.Test
{
    /// <summary>
    /// Interaction logic for TestFileDatabaseCrawl.xaml
    /// </summary>
    public partial class TestFileDatabaseCrawl : Window
    {
        private bool isWorking;

        public TestFileDatabaseCrawl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isWorking)
            {
                Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = isWorking;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var form = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(BrowserTextEdit.Text))
            {
                form.SelectedPath = BrowserTextEdit.Text;
            } else
            {
                form.SelectedPath = @"C:\";
            }
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BrowserTextEdit.Text = form.SelectedPath;
            }
        }

        private void CrawlButton_Click(object sender, RoutedEventArgs e)
        {
            ProgressBar.Visibility = Visibility.Visible;
            isWorking = true;

            
        }
    }
}
