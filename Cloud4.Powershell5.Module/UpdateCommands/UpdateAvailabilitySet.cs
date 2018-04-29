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
    [Cmdlet(VerbsData.Update, "Cloud4AvailabilitySet")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class UpdateAvailabilitySet : BaseCmdLet
    {

        [Parameter(
Mandatory = true,
Position = 0,
ValueFromPipeline = true,
HelpMessage = "Filter by Availability Set Id",
ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
          Mandatory = true,
          Position = 1,
          ValueFromPipeline = true,
            HelpMessage = "Name of the Availability Set",
          ValueFromPipelineByPropertyName = true)]
       
        public string Name { get; set; }





        [Parameter(
      Mandatory = false,
      Position = 2,
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

                string vdcid = Id.ToString("D").ToLower();
                Task<Cloud4.CoreLibrary.Models.AvailabilitySet> callTask = Task.Run(() => service.GetAsync(Id));

                callTask.Wait();
                var vdc = callTask.Result;

                vdc.Name = Name;

                Task<CoreLibrary.Models.Job> callTaskJob = Task.Run(() => service.UpdateAsync(Id, vdc));

                callTaskJob.Wait();
                var job = callTaskJob.Result;
                if (Wait)
                {
                    WaitJobFinished(job.Id);

                  
                    Task<AvailabilitySet> callTasklist = Task.Run(() => service.GetAsync(job.ResourceId));

                    callTasklist.Wait();                    
                    WriteObject(callTasklist.Result);
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
