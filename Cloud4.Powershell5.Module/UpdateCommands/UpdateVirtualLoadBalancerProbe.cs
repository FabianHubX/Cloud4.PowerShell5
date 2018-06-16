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
    [Cmdlet(VerbsData.Update, "Cloud4vLBProbe")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class UpdateVirtualLoadBalancerProbe : BaseLoadBalancerUpdateCmdLet<VirtualLoadBalancerProbe,VirtualLoadBalancerProbeService, Cloud4.CoreLibrary.Models.UpdateVirtualLoadBalancerProbe>
    {
      

    [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Probe Intervall",
          ValueFromPipelineByPropertyName = true)]
      
        public int? IntervalInSeconds { get; set; }

        [Parameter(
        Mandatory = true,
        Position = 1,
        ValueFromPipeline = true,
            HelpMessage = "Protocol",
        ValueFromPipelineByPropertyName = true)]
      
        public LoadBalancerParameters.ProbeProtocol? Protocol { get; set; }

     
        [Parameter(
           Mandatory = true,
           Position = 2,
           ValueFromPipeline = true,
            HelpMessage = "Probe Port",
           ValueFromPipelineByPropertyName = true)]
      
        public int? Port { get; set; }


        [Parameter(
 Mandatory = true,
 Position = 1,
 ValueFromPipeline = true,
   HelpMessage = "Id of the Virtual Load Balancer Probe",
 ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

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

        public int? NumberOfProbes { get; set; }

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

            var vlbnew = new Cloud4.CoreLibrary.Models.UpdateVirtualLoadBalancerProbe();

            if (Protocol.HasValue) { vlbnew.Protocol = Protocol.Value.ToString(); } else { vlbnew.Protocol = vlborg.Protocol; }
            if (IntervalInSeconds.HasValue) { vlbnew.IntervalInSeconds = IntervalInSeconds.Value; } else { vlbnew.IntervalInSeconds = vlborg.IntervalInSeconds; }
            if (NumberOfProbes.HasValue) { vlbnew.NumberOfProbes = NumberOfProbes.Value; } else { vlbnew.NumberOfProbes = vlborg.NumberOfProbes; }


            if (Port.HasValue) { vlbnew.Port = Port.Value; } else { vlbnew.Port = vlborg.Port; }
            if (!string.IsNullOrEmpty(RequestPath))
            {
                vlbnew.RequestPath = RequestPath;
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
