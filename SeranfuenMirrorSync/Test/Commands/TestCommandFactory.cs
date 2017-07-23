using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeranfuenMirrorSync.Test.Commands
{
    public static class TestCommandFactory
    {
        public static RoutedCommand CmdGenerateRandomFolders = new RoutedCommand();
        public static RoutedCommand CmdSyncFolderStructure = new RoutedCommand();
        public static RoutedCommand CmdTestFileCrawler = new RoutedCommand();
        public static RoutedCommand CmdTestSyncFolders = new RoutedCommand();
    }
}
