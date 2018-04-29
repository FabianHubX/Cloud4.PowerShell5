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
    public class NewVirtualLoadBalancerProbe : BaseCmdLet
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

            VirtualLoadBalancerProbeService service = new VirtualLoadBalancerProbeService(Connection, VirtualLoadBalancerId);
      

            try
            {
                var vlb = new CreateVirtualLoadBalancerProbe
                {
                    IntervalInSeconds = IntervalInSeconds,
                     NumberOfProbes = NumberOfProbes,
                      Port = Port,
                      Protocol = Protocol.ToString(),
                      RequestPath = ""

                };
               

                Task<CoreLibrary.Models.Job> callTask = Task.Run(() => service.CreateAsync(vlb));

                callTask.Wait();
                var job = callTask.Result;
                if (Wait)
                {
                    WaitJobFinished(job.Id);
                    Task<List<VirtualLoadBalancerProbe>> callTasklist = Task.Run(() => service.AllAsync());

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
