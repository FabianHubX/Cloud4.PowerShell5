using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateVirtualSubNet
    {
        public Guid VirtualNetworkId { get; set; }

        public CreateVirtualSubNetParams Parameters { get; set; }


    }

    public class CreateVirtualSubNetParams
    {
        public string Name { get; set; }


        public CreateVirtualFirewall4Subnet FirewallCreationParameters { get; set; }
        public string AddressPrefix { get; set; }

        public bool IsGatewaySubnet { get; set; }

    }
}

