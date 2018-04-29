using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualLoadBalancerRule
    {
        public Guid Id { get; set; }
        public Guid BackendAddressPool { get; set; }
        public List<Guid> FrontendIPConfigurations { get; set; }
        public string Protocol { get; set; }
        public int FrontendPort { get; set; }
        public int backendPort { get; set; }
        public int BdleTimeoutInMinutes { get; set; }
        public Guid ProbeId { get; set; }       
        public bool FloatingIpEnabled { get; set; }
        public string LoadDistribution { get; set; }

    }
}
