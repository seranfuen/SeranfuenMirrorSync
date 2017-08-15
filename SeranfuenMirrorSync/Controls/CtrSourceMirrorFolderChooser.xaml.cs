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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeranfuenMirrorSync.Controls
{
    /// <summary>
    /// Interaction logic for CtrSourceMirrorFolderChooser.xaml
    /// </summary>
    public partial class CtrSourceMirrorFolderChooser : UserControl
    {
        public static readonly DependencyProperty SourceFolderPathProperty =
            DependencyProperty.Register("SourceFolderPath", typeof(string), typeof(CtrSourceMirrorFolderChooser), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnSourceFolderPathPropertyChanged)) { BindsTwoWayByDefault = true });

        public static readonly DependencyProperty MirrorFolderPathProperty =
            DependencyProperty.Register("MirrorFolderPath", typeof(string), typeof(CtrSourceMirrorFolderChooser), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnMirrorFolderPathPropertyChanged)) { BindsTwoWayByDefault = true });

        public string SourceFolderPath
        {
            get { return (string)GetValue(SourceFolderPathProperty); }
            set { SetValue(SourceFolderPathProperty, value); }
        }

        public string MirrorFolderPath
        {
            get { return (string)GetValue(MirrorFolderPathProperty); }
            set { SetValue(MirrorFolderPathProperty, value); }
        }

        public CtrSourceMirrorFolderChooser()
        {
            InitializeComponent();
        }

        static void OnSourceFolderPathPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var obj = o as CtrFolderChooseTextBox;
            if (obj == null)
                return;
        }

        static void OnMirrorFolderPathPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var obj = o as CtrFolderChooseTextBox;
            if (obj == null)
                return;
        }
    }
}
