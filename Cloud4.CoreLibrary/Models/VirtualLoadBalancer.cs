﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualLoadBalancer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid VirtualDatacenterId { get; set; }
        public List<Guid> FrontEndIPConfigurations { get; set; }
        public List<Guid> BackendAddressPools { get; set; }
        public List<Guid> Probes { get; set; }
        public List<Guid> InboundNatRules { get; set; }
        public List<Guid> LoadBalancingRules { get; set; }
      
    }

    public class ExtendedVirtualLoadBalancer : VirtualLoadBalancer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid VirtualDatacenterId { get; set; }
        public List<VirtualLoadBalancerFrontEndIPConfigurations> FrontEndIPConfigurations { get; set; }
        public List<VirtualLoadBalancerBackEndPool> BackendAddressPools { get; set; }
        public List<VirtualLoadBalancerProbe> Probes { get; set; }
        public List<VirtualLoadBalancerInboundNatRule> InboundNatRules { get; set; }
        public List<VirtualLoadBalancerRule> LoadBalancingRules { get; set; }

    }
}
