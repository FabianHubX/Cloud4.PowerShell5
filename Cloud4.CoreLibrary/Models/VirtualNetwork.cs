using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualNetwork
    {
        public Guid Id { get; set; }

        public Guid VirtualDatacenterId { get; set; }

        public string[] AddressSpace { get; set; }

        public string Name { get; set; }

        public string[] DnsServers { get; set; }


    }
}


//{
//  "virtualDatacenterId": "string",
//  "addressSpace": [
//    "string"
//  ],
//  "name": "string",
//  "subnets": [
//    {
//      "name": "string",
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
//    }
//  ]
//}