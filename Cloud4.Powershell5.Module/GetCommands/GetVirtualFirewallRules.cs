using Cloud4.CoreLibrary.Models;
using Cloud4.CoreLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module
{
    [Cmdlet(VerbsCommon.Get, "Cloud4vFirewallRule")]
    [OutputType(typeof(VirtualFirewallRule))]
    public class GetVirtualFirewallRules : BaseTenantGetCmdLet<VirtualFirewall, VirtualFirewallService>
    {
        [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true,
           HelpMessage = "Filter by Virtual Firewall Id",
        ValueFromPipelineByPropertyName = true)]
        public Guid VirtualFirewallId { get; set; }

        [Parameter(
        Mandatory = false,
        Position = 0,
        ValueFromPipeline = true,
           HelpMessage = "Filter by Virtual Firewall Rule Id",
        ValueFromPipelineByPropertyName = true)]
        public Guid Id { get; set; }

        private VirtualFirewallService service { get; set; }

        protected override void ProcessRecord()
        {

            WriteObject(GetOne(VirtualFirewallId, Connection).Rules);
          
        }
    
    }
}
