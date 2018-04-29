using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class VirtualLoadBalancerProbeService : BaseLoadBalancerService<VirtualLoadBalancerProbe, CreateVirtualLoadBalancerProbe, VirtualLoadBalancerProbe>
    {
        public VirtualLoadBalancerProbeService()
        {

        }
        public VirtualLoadBalancerProbeService(Connection con, Guid LoadBalancerId) : base(con, LoadBalancerId)
        {
            this.Entity = "LoadBalancers";
            this.SubEntity = "probes";
           
        }





    }
}
