﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class UpdateVirtualLoadBalancerFrontEndIPConfigurations
    {
   
        public Guid? VirtualSubnetId { get; set; }
     
        public string InternalIpAddress { get; set; }


    }
}
