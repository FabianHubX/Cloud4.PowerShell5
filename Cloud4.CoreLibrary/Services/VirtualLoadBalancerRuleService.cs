// Copyright (c) HIAG Data AG. All Rights Reserved. Licensed under the GNU License.  See License.txt
using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class VirtualLoadBalancerRuleService : BaseLoadBalancerService<VirtualLoadBalancerRule, CreateVirtualLoadBalancerRule, VirtualLoadBalancerRule>
    {
        public VirtualLoadBalancerRuleService()
        {

        }
        public VirtualLoadBalancerRuleService(Connection con, Guid LoadBalancerId) : base(con, LoadBalancerId)
        {
            this.Entity = "LoadBalancers";
            this.SubEntity = "loadBalancingRules";
           
        }





    }
}
