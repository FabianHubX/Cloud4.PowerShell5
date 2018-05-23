using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualSubNet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid VirtualNetworkId { get; set; }

        public Guid? VirtualFirewallId { get; set; }

        public string AddressPrefix { get; set; }

        public string NextFreeIpAddress { get; set; }

        public bool IsGatewaySubnet { get; set; }


        //        "id": "string",
        //"virtualNetworkId": "string",
        //"virtualFirewallId": "string"

        //        "id": "string",
        //"virtualDatacenterId": "string"
        //  "name": "string",
        //      "addressPrefix": "string",
        //      "firewallCreationParameters": {
        //        "id": "string",
        //        "rules": [
        //          {
        //            "sourceAddressPrefix": "string",
        //            "sourcePortRange": "string",
        //            "destinationAddressPrefix": "string",
        //            "destinationPortRange": "string",
        //            "protocol": "tcp",
        //            "direction": "inbound",
        //            "action": "allow",
        //            "priority": 0
        //          }
        //        ]
        //      }
    }
}
