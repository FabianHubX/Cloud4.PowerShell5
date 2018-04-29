using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualLoadBalancerFrontEndIPConfigurations
    {
        public Guid Id { get; set; }
        public string PublicIpAddress { get; set; }
        public Guid? VirtualSubnetId { get; set; }
        public string InternalIpAddress { get; set; }


    }
}
