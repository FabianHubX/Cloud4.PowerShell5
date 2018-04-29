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
    public class NewVirtualLoadBalancerRule : BaseCmdLet
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
       Position = 5,
       ValueFromPipeline = true,
         HelpMessage = "Enable Floating IP",
       ValueFromPipelineByPropertyName = true)]

        public bool FloatingIpEnabled { get; set; }

        [Parameter(
       Mandatory = true,
       Position = 5,
       ValueFromPipeline = true,
         HelpMessage = "Load Distribution Type",
       ValueFromPipelineByPropertyName = true)]

        public LoadBalancerParameters.LoadDistribution LoadDistribution { get; set; }

        [Parameter(
       Mandatory = true,
       Position = 5,
       ValueFromPipeline = true,
         HelpMessage = "Probe Id",
       ValueFromPipelineByPropertyName = true)]

        public Guid ProbeId { get; set; }

        [Parameter(
         Mandatory = false,
         Position = 6,
         ValueFromPipeline = true,
          HelpMessage = "Wait Job Finished",
         ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }



        protected override void ProcessRecord()
        {

            VirtualLoadBalancerRuleService service = new VirtualLoadBalancerRuleService(Connection, VirtualLoadBalancerId);
      

            try
            {
                var vlb = new CreateVirtualLoadBalancerRule
                {
                    BackendAddressPool = BackendAddressPool,
                    FrontendIPConfigurations = FrontendIPConfigurations,
                    Protocol = Protocol.ToString(),
                    FrontendPort = FrontEndPort,
                    BackendPort = BackEndPort,
                    IdleTimeoutInMinutes = 0,
                    FloatingIpEnabled = FloatingIpEnabled,
                    LoadDistribution = LoadDistribution.ToString(),
                    ProbeId = ProbeId

                };
               

                Task<CoreLibrary.Models.Job> callTask = Task.Run(() => service.CreateAsync(vlb));

                callTask.Wait();
                var job = callTask.Result;
                if (Wait)
                {
                    WaitJobFinished(job.Id);
                    Task<List<VirtualLoadBalancerRule>> callTasklist = Task.Run(() => service.AllAsync());

                    callTasklist.Wait();
                    var virtualnetworks = callTasklist.Result;

                    WriteObject(virtualnetworks.FirstOrDefault(x => x.Id == job.ResourceId));
                }
                else
                {
                    WriteObject(job);
                }



            }
            catch (Exception e)
            {
                throw new RemoteException("An API Error has happen");
            }

        }

        protected override void EndProcessing()
        {

        }
    }
}
