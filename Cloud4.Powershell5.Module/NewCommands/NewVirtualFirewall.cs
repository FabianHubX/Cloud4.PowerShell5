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
    [Cmdlet(VerbsCommon.New, "Cloud4vFirewall")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualFirewall : BaseCmdLet
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Name of the new virtual Firewall",
          ValueFromPipelineByPropertyName = true)]
      
        public string Name { get; set; }

      

     
        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Datacenter where the Firewall gets created",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid VirtualDataCenterId { get; set; }

        [Parameter(
   Mandatory = true,
   Position = 1,
   ValueFromPipeline = true,
    HelpMessage = "Firewall Rule Set",
   ValueFromPipelineByPropertyName = true)]

        public List<VirtualFirewallRule> Rules { get; set; }

        [Parameter(
     Mandatory = false,
     Position = 4,
     ValueFromPipeline = true,
      HelpMessage = "Wait Job Finished",
     ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }


        private VirtualFirewallService service { get; set; }



        protected override void ProcessRecord()
        {


            service = new VirtualFirewallService(Connection);
           

            try
            {
           

                Task<CoreLibrary.Models.Job> callTask = Task.Run(() => service.CreateAsync(new VirtualFirewall { VirtualDatacenterId = VirtualDataCenterId, Name = Name, Rules = Rules }));

                callTask.Wait();
                var job = callTask.Result;
                if (Wait)
                {
                    WaitJobFinished(job.Id);

                    Task<VirtualFirewall> callTasklist = Task.Run(() => service.GetAsync(job.ResourceId));

                    callTasklist.Wait();
                    var virtualnetworks = callTasklist.Result;

                    WriteObject(virtualnetworks);
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
