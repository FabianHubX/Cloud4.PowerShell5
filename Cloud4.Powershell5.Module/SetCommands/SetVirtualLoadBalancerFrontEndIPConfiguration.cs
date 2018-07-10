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
    [Cmdlet(VerbsCommon.Set, "Cloud4vLBFrontEndIPConfigurations")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class SetVirtualLoadBalancerFrontEndIPConfiguration: BaseLoadBalancerUpdateCmdLet<VirtualLoadBalancerFrontEndIPConfigurations, VirtualLoadBalancerFrontendIpConfigurationsService, Cloud4.CoreLibrary.Models.UpdateVirtualLoadBalancerFrontEndIPConfigurations>
    {


        [Parameter(
         Mandatory = true,
         Position = 0,
         ValueFromPipeline = true,
           HelpMessage = "Id of the Virtual Load Balancer",
         ValueFromPipelineByPropertyName = true)]

        public Guid VirtualLoadBalancerId { get; set; }

        [Parameter(
         Mandatory = false,
         Position = 1,
         ValueFromPipeline = true,
           HelpMessage = "Internal IP Adress",
         ValueFromPipelineByPropertyName = true)]

        public string InternalIpAddress { get; set; }

  
        [Parameter(
       Mandatory = true,
       Position = 3,
       ValueFromPipeline = true,
         HelpMessage = "Virtual Subnet Id",
       ValueFromPipelineByPropertyName = true)]

        public Guid VirtualSubnetId { get; set; }

        [Parameter(
      Mandatory = true,
      Position = 1,
      ValueFromPipeline = true,
        HelpMessage = "Id of the Virtual Load Balancer FrontEnd IP Configuration",
      ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }


        [Parameter(
         Mandatory = false,
         Position = 4,
         ValueFromPipeline = true,
          HelpMessage = "Wait Job Finished",
         ValueFromPipelineByPropertyName = true)]

        public SwitchParameter Wait { get; set; }



        protected override void ProcessRecord()
        {
            var vlborg = Get(Connection, Id, VirtualLoadBalancerId);

            var vlbnew = new Cloud4.CoreLibrary.Models.UpdateVirtualLoadBalancerFrontEndIPConfigurations();
            
          
            if (string.IsNullOrEmpty(InternalIpAddress)) { vlbnew.InternalIpAddress = vlborg.InternalIpAddress; } else { vlbnew.InternalIpAddress = InternalIpAddress; }
            if (VirtualSubnetId == Guid.Empty)
            {
                if (vlborg.VirtualSubnetId.HasValue)
                {
                    vlbnew.VirtualSubnetId = vlborg.VirtualSubnetId;
                }
            }
            else
            {
                vlbnew.VirtualSubnetId = VirtualSubnetId;
            }




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
