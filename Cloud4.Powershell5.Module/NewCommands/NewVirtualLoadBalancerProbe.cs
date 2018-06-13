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
    [Cmdlet(VerbsCommon.New, "Cloud4vLBProbe")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualLoadBalancerProbe : BaseNewLoadBalancerCmdLet<VirtualLoadBalancerProbe,VirtualLoadBalancerProbeService, CreateVirtualLoadBalancerProbe>
    {
      

    [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Probe Intervall",
          ValueFromPipelineByPropertyName = true)]
      
        public int IntervalInSeconds { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 1,
        ValueFromPipeline = true,
            HelpMessage = "Protocol",
        ValueFromPipelineByPropertyName = true)]
      
        public LoadBalancerParameters.ProbeProtocol Protocol { get; set; }

     
        [Parameter(
           Mandatory = true,
           Position = 2,
           ValueFromPipeline = true,
            HelpMessage = "Probe Port",
           ValueFromPipelineByPropertyName = true)]
      
        public int Port { get; set; }
        

        [Parameter(
         Mandatory = true,
         Position = 3,
         ValueFromPipeline = true,
           HelpMessage = "Id of the Virtual Load Balancer",
         ValueFromPipelineByPropertyName = true)]

        public Guid VirtualLoadBalancerId { get; set; }

  

        [Parameter(
       Mandatory = true,
       Position = 4,
       ValueFromPipeline = true,
         HelpMessage = "Path for Request",
       ValueFromPipelineByPropertyName = true)]

        public string RequestPath { get; set; }

        [Parameter(
       Mandatory = true,
       Position = 5,
       ValueFromPipeline = true,
         HelpMessage = "Number of Probes",
       ValueFromPipelineByPropertyName = true)]

        public int NumberOfProbes { get; set; }

        [Parameter(
         Mandatory = false,
         Position = 6,
         ValueFromPipeline = true,
          HelpMessage = "Wait Job Finished",
         ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }



        protected override void ProcessRecord()
        {
            var vlb = new CreateVirtualLoadBalancerProbe
            {
                IntervalInSeconds = IntervalInSeconds,
                NumberOfProbes = NumberOfProbes,
                Port = Port,
                Protocol = Protocol.ToString(),
                RequestPath = RequestPath

            };

            var job = Create(Connection, vlb, VirtualLoadBalancerId);

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
