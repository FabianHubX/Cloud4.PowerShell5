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
    [Cmdlet(VerbsData.Update, "Cloud4vDC")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class UpdateVirtualDC : BaseCmdLet
    {

        [Parameter(
Mandatory = true,
Position = 0,
ValueFromPipeline = true,
HelpMessage = "Filter by vDC Id",
ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
          Mandatory = true,
          Position = 1,
          ValueFromPipeline = true,
            HelpMessage = "Name of the Datacenter",
          ValueFromPipelineByPropertyName = true)]
       
        public string Name { get; set; }





        [Parameter(
      Mandatory = false,
      Position = 2,
      ValueFromPipeline = true,
       HelpMessage = "Wait Job Finished",
      ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }

        private VirtualDataCenterService virtualDataCenterService { get; set; }


        protected override void ProcessRecord()
        {
            virtualDataCenterService = new VirtualDataCenterService(Connection);

            try
            {

                string vdcid = Id.ToString("D").ToLower();
                Task<Cloud4.CoreLibrary.Models.VirtualDatacenter> callTask = Task.Run(() => virtualDataCenterService.GetAsync(Id));

                callTask.Wait();
                var vdc = callTask.Result;

                vdc.Name = Name;

                Task<CoreLibrary.Models.Job> callTaskJob = Task.Run(() => virtualDataCenterService.UpdateAsync(Id, vdc));

                callTaskJob.Wait();
                var job = callTaskJob.Result;
                if (Wait)
                {
                    WaitJobFinished(job.Id);

                  
                    Task<VirtualDatacenter> callTasklist = Task.Run(() => virtualDataCenterService.GetAsync(job.ResourceId));

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
