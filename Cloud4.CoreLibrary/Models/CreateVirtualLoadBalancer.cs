using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateVirtualLoadBalancer
    {      
        public string Name { get; set; }
        public Guid VirtualDatacenterId { get; set; }
        public List<CreateVirtualLoadBalancerFrontEndIPConfigurations> FrontEndIPConfigurations { get; set; }
        public List<CreateVirtualLoadBalancerBackEndPool> BackendAddressPools { get; set; }
        public List<CreateVirtualLoadBalancerProbe> Probes { get; set; }
       
      
    }
}
