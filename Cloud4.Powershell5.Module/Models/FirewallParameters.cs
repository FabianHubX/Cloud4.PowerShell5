using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module.Models
{
    public static class FirewallParameters
    {
        public enum Protocol
        {
            TCP = 1,
            UDP = 2,
            All = 3
        }

        public enum Direction
        {
            Inbound = 1,
            Outbound = 2
        }

        public enum Action
        {
            Allow = 1,
            Deny = 2
        }

    }
}
