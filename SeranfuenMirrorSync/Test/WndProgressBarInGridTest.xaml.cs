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
    /// Interaction logic for WndProgressBarInGridTest.xaml
    /// </summary>
    public partial class WndProgressBarInGridTest : Window
    {
        private ProgressTest _test;

        public WndProgressBarInGridTest()
        {
            InitializeComponent();
            _test = new ProgressTest();
            DataContext = new List<ProgressTest>()
            {
                _test
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _test.StartTest();
        }
    }
}
