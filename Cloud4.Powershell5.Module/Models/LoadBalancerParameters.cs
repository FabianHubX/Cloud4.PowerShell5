using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module.Models
{
    public static class LoadBalancerParameters
    {
        public enum Protocol
        {
            TCP = 1,
            UDP = 2,
            GRE = 3,
            ESP = 4,
            ALL = 5
        }

        public enum LoadDistribution
        {
            Default =1,
            SourceIP = 2,
            SourceIPProtocol = 3
        }
        

        public enum ProbeProtocol
        {
            TCP = 1,
            HTTP = 2
        }
    }
}
