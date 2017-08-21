using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeranfuenMirrorSync.Commands
{
    public static class CommandsFactory
    {
        public static RoutedCommand CmdCancelSync = new RoutedCommand();
        public static RoutedCommand CmdCloseWindow = new RoutedCommand();
    }
}
