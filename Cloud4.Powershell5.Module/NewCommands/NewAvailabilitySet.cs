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
    [Cmdlet(VerbsCommon.New, "Cloud4AvailabilitySet")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewAvailabilitySet : BaseCmdLet
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Name of the new AvailabilitySet",
          ValueFromPipelineByPropertyName = true)]
      
        public string Name { get; set; }

      
     
        [Parameter(
           Mandatory = true,
           Position = 2,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Datacenter where the AvailabilitySet gets created",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid VirtualDataCenterId { get; set; }
              

        [Parameter(
     Mandatory = false,
     Position = 4,
     ValueFromPipeline = true,
      HelpMessage = "Wait Job Finished",
     ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }


        private AvailabilitySetService service { get; set; }



        protected override void ProcessRecord()
        {

            service = new AvailabilitySetService(Connection);
          

            try
            {
               

                Task<CoreLibrary.Models.Job> callTask = Task.Run(() => service.CreateAsync(new AvailabilitySet { Name = Name, VirtualDatacenterId = VirtualDataCenterId }));

                callTask.Wait();
                var job = callTask.Result;
                if (Wait)
                {
                    WaitJobFinished(job.Id);
                    Task<List<AvailabilitySet>> callTasklist = Task.Run(() => service.AllAsync());

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
