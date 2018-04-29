using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualLoadBalancerBackEndPool
    {
        public Guid Id { get; set; }
        public List<Guid> VirtualMachines { get; set; }

    }
}
