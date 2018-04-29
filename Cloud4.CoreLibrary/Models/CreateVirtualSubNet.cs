using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateVirtualSubNet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid VirtualNetworkId { get; set; }

        public Guid? VirtualFirewallId { get; set; }

        public string AddressPrefix { get; set; }

        public string NextFreeIpAddress { get; set; }
   


}
}
