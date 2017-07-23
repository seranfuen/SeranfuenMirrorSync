using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for RandomFolderGenerator.xaml
    /// </summary>
    public partial class WndRandomFolderGenerator : Window
    {
        public WndRandomFolderGenerator()
        {
            InitializeComponent();
        }

        private int Breadth
        {
            get
            {
                try
                {
                    return Int32.Parse(BreadthTextBox.Text);
                }
                catch
                {
                    return 0;
                }
            }
        }

        private int Depth
        {
            get
            {
                try
                {
                    return Int32.Parse(DepthTextBox.Text);
                }
                catch
                {
                    return 0;
                }
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new FolderBrowserDialog();
            if (string.IsNullOrWhiteSpace(FolderTextBox.Text))
            {
                form.SelectedPath = @"C:\";
            }
            else
            {
                form.SelectedPath = FolderTextBox.Text;
            }

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderTextBox.Text = form.SelectedPath;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProgressBar.Visibility = Visibility.Visible;
            StatusLabel.Content = "Running random generator";
            var controller = new SeranfuenMirrorSyncLib.Controllers.RandomFolderGenerator(FolderTextBox.Text, Depth, Breadth);

            Task.Factory.StartNew(() =>
            {
                controller.Start();
            }).ContinueWith((task) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (task.IsFaulted)
                    {
                        System.Windows.MessageBox.Show(string.Format("The following errors ocurred: \n\n'{0}'", task.Exception.InnerException.Message), "Folder generation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        StatusLabel.Content = string.Format("Generation failed. {0} folders were created", controller.GeneratedFoldersCount);
                    }
                    else
                    {
                        StatusLabel.Content = string.Format("Generation completed. {0} folders were created", controller.GeneratedFoldersCount);
                    }
                    ProgressBar.Visibility = Visibility.Hidden;
                });
            });
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var hasData = Breadth > 0 && Depth > 0;
            RunButton.IsEnabled = hasData;
            if (hasData)
            {
                StatusLabel.Content = string.Format("{0:#,##0} folders will be generated", Math.Pow(Breadth, Depth + 1) - 1);
            }
        }
    }
}
