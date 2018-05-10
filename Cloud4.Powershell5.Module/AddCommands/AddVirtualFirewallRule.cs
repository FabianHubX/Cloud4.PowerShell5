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
    [Cmdlet(VerbsCommon.Add, "Cloud4vFirewallRule")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class AddVirtualFirewallRule : BaseAddCmdLet<VirtualFirewall, VirtualFirewallService>
    {

       
      

        [Parameter(
           Mandatory = true,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Firewall where the Rule gets added",
           ValueFromPipelineByPropertyName = true)]

        public Guid VirtualFirewallId { get; set; }


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
     Position = 9,
     ValueFromPipeline = true,
      HelpMessage = "Wait Job Finished",
     ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }


        protected override void ProcessRecord()
        {
            VirtualFirewallService service = new VirtualFirewallService(Connection);

            var currentvirtualFirewall = Get(Connection, VirtualFirewallId);

            var virtualFirewall = new Cloud4.CoreLibrary.Models.UpdateVirtualFirewall();
            virtualFirewall.Rules = currentvirtualFirewall.Rules;
            virtualFirewall.Name = currentvirtualFirewall.Name;



            if (virtualFirewall.Rules == null)
            {
                virtualFirewall.Rules = new List<VirtualFirewallRule>();
            }

            var rule = new VirtualFirewallRule
            {
                SourceAddressPrefix = SourceAddressPrefix,
                SourcePortRange = SourcePortRange,
                DestinationAddressPrefix = DestinationAddressPrefix,
                DestinationPortRange = DestinationPortRange,
                Direction = Direction.ToString(),
                Action = Action.ToString(),
                Priority = Priority,
                Protocol = Protocol.ToString()

            };

            virtualFirewall.Rules.Add(rule);


            Task<CoreLibrary.Models.Result> callTask = Task.Run(() => service.UpdateAsync(VirtualFirewallId, virtualFirewall));

            callTask.Wait();
            var result = callTask.Result;
            CoreLibrary.Models.Job job;

            if (result.Job != null)
            {
                job = result.Job;
            }
            else if (result.Error != null)
            {
                throw new RemoteException("Conflict Error: " + result.Error.ErrorType + "\r\n" + result.Error.FaultyValues);
            }
            else
            {
                throw new RemoteException("API returns: " + result.Code.ToString());
            }

            if (Wait)
            {
                WriteObject(WaitJobFinished(job.Id,Connection));             
            }
            else
            {
                WriteObject(job);
            }

        }

        
        
    }
}
