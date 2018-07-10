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
    [Cmdlet(VerbsCommon.Set, "Cloud4vLBInboundNATRule")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class SetVirtualLoadBalancerInboundNatRule : BaseLoadBalancerUpdateCmdLet<VirtualLoadBalancerInboundNatRule, VirtualLoadBalancerInboundNatRuleService, Cloud4.CoreLibrary.Models.UpdateVirtualLoadBalancerInboundNatRule>
    {
      

 

        [Parameter(
        Mandatory = false,
        Position = 1,
        ValueFromPipeline = true,
            HelpMessage = "Protocol",
        ValueFromPipelineByPropertyName = true)]
      
        public LoadBalancerParameters.Protocol? Protocol { get; set; }

     
        [Parameter(
           Mandatory = false,
           Position = 2,
           ValueFromPipeline = true,
            HelpMessage = "FrontEnd Port",
           ValueFromPipelineByPropertyName = true)]
      
        public int? FrontEndPort { get; set; }

        [Parameter(
        Mandatory = false,
        Position = 3,
        ValueFromPipeline = true,
            HelpMessage = "BackEnd Port",
        ValueFromPipelineByPropertyName = true)]

        public int? BackEndPort { get; set; }



        [Parameter(
 Mandatory = true,
 Position = 1,
 ValueFromPipeline = true,
   HelpMessage = "Id of the Virtual Load Balancer BackendPool",
 ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
         Mandatory = true,
         Position = 5,
         ValueFromPipeline = true,
           HelpMessage = "Id of the Virtual Load Balancer",
         ValueFromPipelineByPropertyName = true)]

        public Guid VirtualLoadBalancerId { get; set; }

        [Parameter(
         Mandatory = false,
         Position = 6,
         ValueFromPipeline = true,
          HelpMessage = "Wait Job Finished",
         ValueFromPipelineByPropertyName = true)]

        public SwitchParameter Wait { get; set; }



        protected override void ProcessRecord()
        {
            var vlborg = Get(Connection, Id, VirtualLoadBalancerId);

            var vlbnew = new Cloud4.CoreLibrary.Models.UpdateVirtualLoadBalancerInboundNatRule();

            if (Protocol.HasValue) { vlbnew.Protocol = Protocol.Value.ToString(); } else { vlbnew.Protocol = vlborg.Protocol; }
            if (FrontEndPort.HasValue) { vlbnew.FrontendPort = FrontEndPort.Value; } else { vlbnew.FrontendPort = vlborg.FrontendPort; }
            if (BackEndPort.HasValue) { vlbnew.BackendPort = BackEndPort.Value; } else { vlbnew.BackendPort = vlborg.BackendPort; }



            var job = Update(Connection, Id, vlbnew, VirtualLoadBalancerId);


            if (Wait)
            {
                WriteObject(WaitJobFinished(job.Id, Connection, VirtualLoadBalancerId));
            }
            else
            {
                WriteObject(job);
            }

        }
    
    }
}
