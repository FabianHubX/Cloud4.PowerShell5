using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateVirtualNetwork
    {
   

        public Guid VirtualDatacenterId { get; set; }

        public string[] AddressSpace { get; set; }

        public string Name { get; set; }

        public string[] DnsServers { get; set; }


        public List<CreateVirtualSubNet> SubNets { get; set; }


    }
}

