using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateVirtualLoadBalancerRule
    {
  
        public Guid BackendAddressPool { get; set; }
        public List<Guid> FrontendIPConfigurations { get; set; }
        public string Protocol { get; set; }   // TCP,UDP,GRE,ESP,ALL
        public int FrontendPort { get; set; }
        public int BackendPort { get; set; }
        public int IdleTimeoutInMinutes { get; set; }  // 4  (4-30
        public Guid ProbeId { get; set; }       
        public bool EnableFloatingIp  { get; set; }    // if true FrontEnd==BackPort
        public string LoadDistribution { get; set; }    //  Default, SourceIP,  SourceIPProtocol



    }
}
