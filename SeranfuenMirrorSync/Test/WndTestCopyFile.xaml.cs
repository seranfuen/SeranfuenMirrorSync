using SeranfuenMirrorSyncLib.Controllers;
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
using SeranfuenMirrorSync.Controllers;

namespace SeranfuenMirrorSync.Test
{
    /// <summary>
    /// Interaction logic for WndTestCopyFile.xaml
    /// </summary>
    public partial class WndTestCopyFile : Window
    {
        private bool _working = false;
        private ProgressCopyController _controller;

        public WndTestCopyFile()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SourceTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var textbox = (System.Windows.Controls.TextBox)sender;
            var form = new OpenFileDialog();
            form.FileName = string.IsNullOrWhiteSpace(textbox.Text) ? null : textbox.Text;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textbox.Text = form.FileName;
            }
        }

        private void CmdCopy_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SourceTextBox.Text) || string.IsNullOrWhiteSpace(DestinationTextBox.Text))
            {
                System.Windows.MessageBox.Show("You must select a source and a destination", "No source/destination", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                CmdCopy.IsEnabled = false;
                CmdCancel.IsEnabled = true;
                var source = SourceTextBox.Text;
                var destination = DestinationTextBox.Text;
                var overwriteFile = OverwriteFileCheckBox.IsChecked.HasValue ? OverwriteFileCheckBox.IsChecked.Value : false;
                var updateTimestamps = TimestampsCheckBox.IsChecked.HasValue ? TimestampsCheckBox.IsChecked.Value : false;

                Task.Factory.StartNew(() =>
                {
                    _controller = new ProgressCopyController(source, destination);
                    _controller.ProgressCopyEvent += Controller_ProgressCopyEvent;
                    _controller.UpdateTimestamps = updateTimestamps;
                    _controller.OverwriteFile = overwriteFile;
                    _controller.StartCopy();
                }).ContinueWith((task) =>
                {
                    _working = false;
                    Dispatcher.Invoke(() =>
                    {
                        CmdCopy.IsEnabled = true;
                        CmdCancel.IsEnabled = false;
                    });
                });
            }
        }

        private void Controller_ProgressCopyEvent(object sender, SeranfuenMirrorSyncLib.Data.ProgressCopyEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e.Status.Failed)
                {
                    StatusLabel.Content = string.Format("File copy failed with '{0}'", e.Status.Exception.Message);
                }
                else if (e.Status.Canceled)
                {
                    StatusLabel.Content = "File copy was canceled";
                }
                else if (e.Status.Finished)
                {
                    StatusLabel.Content = string.Format("File copy was completed in {0:#0} seconds", (e.Status.End.Value - e.Status.Start.Value).TotalSeconds);
                }
                else
                {
                    StatusLabel.Content = string.Format("Copying file {0}/s", e.Status.AverageSpeedBps.ToSizeString());
                    ProgressBar.Maximum = (double)e.Status.TotalSize;
                    ProgressBar.Value = (double)e.Status.CopiedSize;
                }
            });
        }

        private void DestinationTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var textbox = (System.Windows.Controls.TextBox)sender;
            var form = new SaveFileDialog();
            if (string.IsNullOrWhiteSpace(textbox.Text))
            {
                if (!string.IsNullOrWhiteSpace(SourceTextBox.Text))
                {
                    form.FileName = System.IO.Path.GetFileName(SourceTextBox.Text);
                }
            }
            else
            {
                form.FileName = textbox.Text;
            }
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textbox.Text = form.FileName;
            }
        }

        private void CmdCancel_Click(object sender, RoutedEventArgs e)
        {
            _controller.Cancel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = _working;
        }
    }
}
