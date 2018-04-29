using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Models
{
    public class VirtualFirewallRule
    {
        public string SourceAddressPrefix { get; set; }
        public string SourcePortRange { get; set; }
        public string DestinationAddressPrefix { get; set; }

        public string DestinationPortRange { get; set; }
        public string Protocol { get; set; }
        public string Direction { get; set; }
        public string Action { get; set; }
        public int Priority { get; set; }
     

    }


}
