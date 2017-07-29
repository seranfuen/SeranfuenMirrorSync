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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeranfuenMirrorSync.Controls
{
    /// <summary>
    /// Lógica de interacción para CtrFolderChooseTextBox.xaml
    /// </summary>
    public partial class CtrFolderChooseTextBox : System.Windows.Controls.UserControl
    {
        public CtrFolderChooseTextBox()
        {
            InitializeComponent();
        }

        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPathProperty =
            DependencyProperty.Register("SelectedPath", typeof(string), typeof(CtrFolderChooseTextBox), new PropertyMetadata(0));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new FolderBrowserDialog();
            if (!string.IsNullOrWhiteSpace(FolderPathTextBox.Text))
            {
                form.SelectedPath = FolderPathTextBox.Text;
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                FolderPathTextBox.Text = form.SelectedPath;
            }
        }
    }
}
