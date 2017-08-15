using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
#if DEBUG
            var serviceTest = new MirrorSyncService();
            serviceTest.StartTest(args);
            Console.ReadLine();
            serviceTest.StopTest();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MirrorSyncService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
