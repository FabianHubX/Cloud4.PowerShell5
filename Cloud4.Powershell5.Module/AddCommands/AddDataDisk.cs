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
    [Cmdlet(VerbsCommon.Add, "Cloud4vDataDisk")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class AddDataDisk : BaseCmdLet
    {

        private string _diskProfile;
        private string _name;


        [Parameter(
           Mandatory = true,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Virtual Maschine where the new Data Disk gets assigned too",
           ValueFromPipelineByPropertyName = true)]

        public Guid VirtualMachineId { get; set; }


        [Parameter(
   Mandatory = true,
   Position = 1,
   ValueFromPipeline = true,
    HelpMessage = "Your Virtual Data Disk Name",
   ValueFromPipelineByPropertyName = true)]

        public string Name { get => _name; set => _name = value; }



        [Parameter(
           Mandatory = true,
           Position = 2,
           ValueFromPipeline = true,
            HelpMessage = "Your Virtual Disk Profile",
           ValueFromPipelineByPropertyName = true)]

        public string DiskProfile { get => _diskProfile; set => _diskProfile = value; }



        public bool Wait { get; set; }

        private VirtualDiskService service { get; set; }



        protected override void ProcessRecord()
        {


            service = new VirtualDiskService(Connection);



            var virtualnic = new CreateVirtualDisk
            {
                Name = _name,
                VirtualDiskProfileName = _diskProfile
            };





            Task<CoreLibrary.Models.Job> callTask = Task.Run(() => service.CreateAsync(virtualnic));

            callTask.Wait();
            var job = callTask.Result;
         

            if (Wait)
            {
                JobService jobService = new JobService(Connection);

                WaitJobFinished(job.Id);

                Task<List<VirtualDisk>> callTasklist = Task.Run(() => service.AllAsync());

                callTasklist.Wait();
                var virtualDatacenters = callTasklist.Result;

                WriteObject(virtualDatacenters.FirstOrDefault(x => x.Id == job.ResourceId));
            }
            else
            {

                WriteObject(job);
            }

        }

        protected override void EndProcessing()
        {

        }
    }
}
