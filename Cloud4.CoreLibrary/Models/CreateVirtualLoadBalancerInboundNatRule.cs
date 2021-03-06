﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateVirtualLoadBalancerInboundNatRule
    {
     
        public Guid VirtualMachineId { get; set; }
        public string Protocol { get; set; }   // TCP,UDP,GRE,ESP,ALL
        public int FrontendPort { get; set; }
        public int BackendPort { get; set; }
        public List<Guid> FrontendIpConfigurations { get; set; }


    }
}
