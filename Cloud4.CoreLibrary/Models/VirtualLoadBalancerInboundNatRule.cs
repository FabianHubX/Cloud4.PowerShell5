using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualLoadBalancerInboundNatRule
    {
        public Guid Id { get; set; }
        public Guid VirtualMachineId { get; set; }
        public string Protocol { get; set; }
        public int FrontendPort { get; set; }
        public int BackendPort { get; set; }
        public List<Guid> FrontendIpConfigurations { get; set; }


    }
}
