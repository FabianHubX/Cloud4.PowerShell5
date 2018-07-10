using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class CreateVirtualFirewall4Subnet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<CreateVirtualFirewallRule> Rules { get; set; }

   }
}
