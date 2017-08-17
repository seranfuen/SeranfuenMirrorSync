using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeranfuenMirrorSync.Test.ViewModels
{
    class ProgressTest : INotifyPropertyChanged
    {
        private Timer _timer;
        private int _progress = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProgressTest()
        {

        }

        public void StartTest()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
            _progress = 0;
            _timer = new Timer();
            _timer.Interval = 100;
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        public int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Progress"));
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            Progress++;
            if (Progress >= 100)
            {
                _timer.Stop();
            }
        }
    }
}
