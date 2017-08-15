using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncWcfClient
{
    public static class ServiceProxyFactory
    {
        public static IServiceProxy Proxy
        {
            get
            {
                return new ServiceProxy();
            }
        }
    }
}
