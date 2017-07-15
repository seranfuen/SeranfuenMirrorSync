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
    public partial class RandomFolderGenerator : Window
    {
        public RandomFolderGenerator()
        {
            InitializeComponent();
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
            try
            {
                var controller = new SeranfuenMirrorSyncLib.Controllers.RandomFolderGenerator(FolderTextBox.Text);
                controller.Start();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(string.Format("The following errors ocurred: \n\n'{0}'", ex.Message), "Folder generation error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
    }
}
