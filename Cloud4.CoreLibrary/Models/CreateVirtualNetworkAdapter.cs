using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateVirtualNetworkAdapter
    {
        public Guid SubNetId { get; set; }
    
        public string IpAddress { get; set; }

        public int IpAllocationMethod { get; set; }

        public string VirtualNetworkAdapterProfileName { get; set; }

        public string[] DnsServers { get; set; }

        public VirtualFirewall FirewallCreationParameters { get; set; }


    }
}
