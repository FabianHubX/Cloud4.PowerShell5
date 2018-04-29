using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Models
{
    public class UpdateVirtualFirewall
    {
      
        public string Name { get; set; }       

        public List<VirtualFirewallRule> Rules { get; set; }


    }
}
