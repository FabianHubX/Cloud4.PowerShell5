using Cloud4.CoreLibrary.Models;
using Cloud4.CoreLibrary.Services;
using Cloud4.Powershell5.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module
{
    [Cmdlet(VerbsCommon.New, "Cloud4vSubNet")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualSubNet : BaseNewCmdLet<VirtualSubNet, VirtualSubNetService, CreateVirtualSubNet>
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Name of the new Virtual SubNet",
          ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }

       
        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Network where the virtual SubNet get created",
           ValueFromPipelineByPropertyName = true)]

        public Guid VirtualNetworkId { get; set; }


        [Parameter(
        Mandatory = true,
        Position = 2,
        ValueFromPipeline = true,
            HelpMessage = "Address Space for the virtual Network",
        ValueFromPipelineByPropertyName = true)]

        public string AddressSpace { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 3,
        ValueFromPipeline = true,
            HelpMessage = "Virtual Network is a Gateway Net",
        ValueFromPipelineByPropertyName = true)]

        public bool IsGatewaySubnet { get; set; }

        [Parameter(
   Mandatory = false,
   Position = 4,
   ValueFromPipeline = true,
       HelpMessage = "Assign virtual Firewall to the virtual Network",
   ValueFromPipelineByPropertyName = true)]

        public Guid VirtualFirewallId { get; set; }

        [Parameter(
Mandatory = false,
Position = 5,
ValueFromPipeline = true,
  HelpMessage = "New virtual Firewall Name to the virtual Network",
ValueFromPipelineByPropertyName = true)]

        public string NewVirtualFirewallName { get; set; }


        [Parameter(
Mandatory = false,
Position = 6,
ValueFromPipeline = true,
HelpMessage = "Rules for the new virtual Firewall to the virtual Network",
ValueFromPipelineByPropertyName = true)]

        public List<VirtualFirewallRule> Rules { get; set; }

        [Parameter(
     Mandatory = false,
     Position = 7,
     ValueFromPipeline = true,
      HelpMessage = "Wait Job Finished",
     ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }


        private VirtualSubNetService service { get; set; }


        protected override void ProcessRecord()
        {


            var newfirewall = new CreateVirtualFirewall4Subnet();
            if (VirtualFirewallId != Guid.Empty)
            {
                newfirewall.Id = VirtualFirewallId;
            }
            else if (!string.IsNullOrEmpty(NewVirtualFirewallName))
            {
                newfirewall.Name = NewVirtualFirewallName;
                newfirewall.Rules = Rules;
            }
            else
            {
                newfirewall = null;
            }


            var newsubnet = new CreateVirtualSubNet {
                Name = Name,
                VirtualNetworkId = VirtualNetworkId,
                IsGatewaySubnet = IsGatewaySubnet ,
                AddressPrefix = AddressSpace,
                FirewallCreationParameters = newfirewall
            };

            var job = Create(Connection, newsubnet);

            if (Wait)
            {
                WriteObject(WaitJobFinished(job.Id, Connection));
            }
            else
            {
                WriteObject(job);
            }


        }
        
        
    }
}
