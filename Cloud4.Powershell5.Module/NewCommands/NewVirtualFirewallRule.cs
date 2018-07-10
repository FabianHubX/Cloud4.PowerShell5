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
    [Cmdlet(VerbsCommon.New, "Cloud4vFirewallRule")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualFirewallRule : BaseVirtualFirewallNewCmdLet<VirtualFirewallRule, VirtualFirewallRuleService, CreateVirtualFirewallRule>
    {


        [Parameter(
        Mandatory = true,
        Position = 0,
        ValueFromPipeline = true,
        HelpMessage = "Id of the Virtual Firewall",
        ValueFromPipelineByPropertyName = true)]

        public Guid VirtualFirewallId { get; set; }

        [Parameter(
  Mandatory = true,
  Position = 1,
  ValueFromPipeline = true,
    HelpMessage = "Name of the Firewall Rule",
  ValueFromPipelineByPropertyName = true)]
        public string Name { get; set; }


        [Parameter(
          Mandatory = true,
          Position = 1,
          ValueFromPipeline = true,
            HelpMessage = "Source Address Range x.x.x.x/x or * for Any",
          ValueFromPipelineByPropertyName = true)]
        public string SourceAddressPrefix { get; set; }

        [Parameter(
          Mandatory = true,
          Position = 2,
          ValueFromPipeline = true,
            HelpMessage = "Source Port Range",
          ValueFromPipelineByPropertyName = true)]
        public string SourcePortRange { get; set; }

        [Parameter(
          Mandatory = true,
          Position = 3,
          ValueFromPipeline = true,
            HelpMessage = "Source Address Range x.x.x.x/x or * for Any",
          ValueFromPipelineByPropertyName = true)]
        public string DestinationAddressPrefix { get; set; }

        [Parameter(
          Mandatory = true,
          Position = 4,
          ValueFromPipeline = true,
            HelpMessage = "Source Port Range",
          ValueFromPipelineByPropertyName = true)]
        public string DestinationPortRange { get; set; }


        [Parameter(
         Mandatory = true,
         Position = 5,
         ValueFromPipeline = true,
           HelpMessage = "Protocol TCP or UDP",
         ValueFromPipelineByPropertyName = true)]
        public FirewallParameters.Protocol Protocol { get; set; }

        [Parameter(
         Mandatory = true,
         Position = 6,
         ValueFromPipeline = true,
           HelpMessage = "Direction (Inbound or Outbound)",
         ValueFromPipelineByPropertyName = true)]
        public FirewallParameters.Direction Direction { get; set; }


        [Parameter(
       Mandatory = true,
       Position = 7,
       ValueFromPipeline = true,
         HelpMessage = "Action (Allow or Deny)",
       ValueFromPipelineByPropertyName = true)]
        public FirewallParameters.Action Action { get; set; }

        [Parameter(
     Mandatory = true,
     Position = 8,
     ValueFromPipeline = true,
       HelpMessage = "Priority 100-100000",
     ValueFromPipelineByPropertyName = true)]
        public int Priority { get; set; }

        [Parameter(
        Mandatory = false,
        Position = 17,
        ValueFromPipeline = true,
        HelpMessage = "Wait Job Finished",
        ValueFromPipelineByPropertyName = true)]

        public SwitchParameter Wait { get; set; }



        protected override void ProcessRecord()
        {

            var vgw = new CreateVirtualFirewallRule
            {
                Name = Name,
                SourceAddressPrefix = SourceAddressPrefix,
                SourcePortRange = SourcePortRange,
                DestinationAddressPrefix = DestinationAddressPrefix,
                DestinationPortRange = DestinationPortRange,
                Direction = Direction.ToString(),
                Action = Action.ToString(),
                Priority = Priority,
                Protocol = Protocol.ToString()

            };

            var job = Create(Connection, vgw, VirtualFirewallId);


            if (Wait)
            {
                WriteObject(WaitJobFinished(job.Id, Connection, VirtualFirewallId));
            }
            else
            {
                WriteObject(job);
            }

        }



    }
}
