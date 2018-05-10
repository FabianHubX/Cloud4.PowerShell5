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
    [Cmdlet(VerbsCommon.New, "Cloud4vLBInboundNATRule")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualLoadBalancerInboundNatRule : BaseNewCmdLet<VirtualLoadBalancerInboundNatRule, VirtualLoadBalancerInboundNatRuleService, CreateVirtualLoadBalancerInboundNatRule>
    {
      

    [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Id of the Virtual Machine",
          ValueFromPipelineByPropertyName = true)]
      
        public Guid VirtualMachineId { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 1,
        ValueFromPipeline = true,
            HelpMessage = "Protocol",
        ValueFromPipelineByPropertyName = true)]
      
        public LoadBalancerParameters.Protocol Protocol { get; set; }

     
        [Parameter(
           Mandatory = true,
           Position = 2,
           ValueFromPipeline = true,
            HelpMessage = "FrontEnd Port",
           ValueFromPipelineByPropertyName = true)]
      
        public int FrontEndPort { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 3,
        ValueFromPipeline = true,
            HelpMessage = "BackEnd Port",
        ValueFromPipelineByPropertyName = true)]

        public int BackEndPort { get; set; }


        [Parameter(
      Mandatory = true,
      Position = 4,
      ValueFromPipeline = true,
          HelpMessage = "FrontEnd IP Configuration Ids",
      ValueFromPipelineByPropertyName = true)]
        public List<Guid> FrontendIpConfigurations { get; set; }


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

        public bool Wait { get; set; }



        protected override void ProcessRecord()
        {
            var vlb = new CreateVirtualLoadBalancerInboundNatRule
            {
                BackendPort = BackEndPort,
                FrontendIpConfigurations = FrontendIpConfigurations,
                FrontendPort = FrontEndPort,
                Protocol = Protocol.ToString(),
                VirtualMachineId = VirtualMachineId

            };


            var job = Create(Connection, vlb);

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
