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
    [Cmdlet(VerbsCommon.New, "Cloud4vLBFrontEndIPConfigurations")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualLoadBalancerFrontEndIPConfiguration: BaseNewLoadBalancerCmdLet<VirtualLoadBalancerFrontEndIPConfigurations, VirtualLoadBalancerFrontendIpConfigurationsService, CreateVirtualLoadBalancerFrontEndIPConfigurations>
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
       Mandatory = false,
       Position = 2,
       ValueFromPipeline = true,
         HelpMessage = "Assign a Public IP Address",
       ValueFromPipelineByPropertyName = true)]

        public bool AssignPublicIP { get; set; }

        [Parameter(
       Mandatory = true,
       Position = 3,
       ValueFromPipeline = true,
         HelpMessage = "Virtual Subnet Id",
       ValueFromPipelineByPropertyName = true)]

        public Guid VirtualSubnetId { get; set; }

        [Parameter(
         Mandatory = false,
         Position = 4,
         ValueFromPipeline = true,
          HelpMessage = "Wait Job Finished",
         ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }



        protected override void ProcessRecord()
        {            

            if (string.IsNullOrEmpty(InternalIpAddress))
            {
                var subnet = GetSpecial<VirtualSubNet, VirtualSubNetService>(Connection, VirtualSubnetId);
                InternalIpAddress = subnet.NextFreeIpAddress;
            }
            

            var vlb = new CreateVirtualLoadBalancerFrontEndIPConfigurations
            {
                InternalIpAddress = InternalIpAddress,
                AssignPublicIp = AssignPublicIP,
                VirtualSubnetId = VirtualSubnetId

            };


            var job = Create(Connection, vlb, VirtualLoadBalancerId);

            if (Wait)
            {
                WriteObject(WaitJobFinished(job.Id,Connection,VirtualLoadBalancerId));               
            }
            else
            {
                WriteObject(job);
            }


        }

    }
}
