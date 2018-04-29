using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateVirtualLoadBalancerFrontEndIPConfigurations
    {
   
        public bool AssignPublicIp { get; set; }
        public Guid VirtualSubnetId { get; set; }
        public string InternalIpAddress { get; set; }


    }
}
