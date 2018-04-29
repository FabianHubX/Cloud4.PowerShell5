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
    [Cmdlet(VerbsCommon.New, "Cloud4vDC")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualDC : BaseCmdLet
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
            HelpMessage = "Name of the new Datacenter",
          ValueFromPipelineByPropertyName = true)]
       
        public string Name { get; set; }

       

        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipeline = true,
            HelpMessage = "Region where the virtual Datacenter get created",
           ValueFromPipelineByPropertyName = true)]
      
        public Guid RegionId { get; set; }

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
                
                Task<CoreLibrary.Models.Job> callTask = Task.Run(() => virtualDataCenterService.CreateAsync(new VirtualDatacenter { Name = Name, RegionId = RegionId, TenantId = Connection.TenantId }));

                callTask.Wait();
                var job = callTask.Result;
                if (Wait)
                {
                    WaitJobFinished(job.Id);
                    Task<List<VirtualDatacenter>> callTasklist = Task.Run(() => virtualDataCenterService.AllAsync());

                    callTasklist.Wait();
                    var virtualDatacenters = callTasklist.Result;

                    WriteObject(virtualDatacenters.FirstOrDefault(x => x.Name == Name));
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
