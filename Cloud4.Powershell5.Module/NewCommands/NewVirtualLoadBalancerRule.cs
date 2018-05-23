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
    [Cmdlet(VerbsCommon.New, "Cloud4vLBRule")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualLoadBalancerRule : BaseNewLoadBalancerCmdLet<VirtualLoadBalancerRule, VirtualLoadBalancerRuleService, CreateVirtualLoadBalancerRule>
    {
      

    [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Id of the Virtual Machine",
          ValueFromPipelineByPropertyName = true)]
      
        public Guid BackendAddressPool { get; set; }

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
        public List<Guid> FrontendIPConfigurations { get; set; }


        [Parameter(
         Mandatory = true,
         Position = 5,
         ValueFromPipeline = true,
           HelpMessage = "Id of the Virtual Load Balancer",
         ValueFromPipelineByPropertyName = true)]

        public Guid VirtualLoadBalancerId { get; set; }

        [Parameter(
       Mandatory = true,
       Position = 6,
       ValueFromPipeline = true,
         HelpMessage = "Enable Floating IP",
       ValueFromPipelineByPropertyName = true)]

        public bool FloatingIpEnabled { get; set; }

        [Parameter(
       Mandatory = true,
       Position = 7,
       ValueFromPipeline = true,
         HelpMessage = "Load Distribution Type",
       ValueFromPipelineByPropertyName = true)]

        public LoadBalancerParameters.LoadDistribution LoadDistribution { get; set; }

        [Parameter(
       Mandatory = true,
       Position = 8,
       ValueFromPipeline = true,
         HelpMessage = "Probe Id",
       ValueFromPipelineByPropertyName = true)]

        public Guid ProbeId { get; set; }
        [Parameter(
     Mandatory = true,
     Position = 9,
     ValueFromPipeline = true,
       HelpMessage = "Idle Timeout In Minutes",
     ValueFromPipelineByPropertyName = true)]

        public int IdleTimeoutInMinutes { get; set; }

        [Parameter(
         Mandatory = false,
         Position = 10,
         ValueFromPipeline = true,
          HelpMessage = "Wait Job Finished",
         ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }



        protected override void ProcessRecord()
        {

            var vlb = new CreateVirtualLoadBalancerRule
            {
                BackendAddressPool = BackendAddressPool,
                FrontendIPConfigurations = FrontendIPConfigurations,
                Protocol = Protocol.ToString(),
                FrontendPort = FrontEndPort,
                BackendPort = BackEndPort,
                IdleTimeoutInMinutes = IdleTimeoutInMinutes,
                EnableFloatingIp = FloatingIpEnabled,
                LoadDistribution = LoadDistribution.ToString(),
                ProbeId = ProbeId

            };

            var job = Create(Connection, vlb, VirtualLoadBalancerId);


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
