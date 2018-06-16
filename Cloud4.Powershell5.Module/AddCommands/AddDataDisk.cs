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
    public class AddDataDisk : BaseTenantAddCmdLet<VirtualDisk, VirtualDiskService>
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



        public SwitchParameter Wait { get; set; }

        private VirtualDiskService service { get; set; }



        protected override void ProcessRecord()
        {


            service = new VirtualDiskService(Connection);



            var virtualDisk = new CreateVirtualDisk
            {
                Name = _name,
                VirtualDiskProfileName = _diskProfile,
                virtualMachineId = VirtualMachineId
            };





            Task<CoreLibrary.Models.Result> callTask = Task.Run(() => service.CreateAsync(virtualDisk));

            callTask.Wait();
            var result = callTask.Result;
            CoreLibrary.Models.Job job;

            if (result.Job != null)
            {
                job = result.Job;
            }
            else if (result.Error != null)
            {
                throw new RemoteException("Conflict Error: " + result.Error.ErrorType + "\r\n" + result.Error.FaultyValues);
            }
            else
            {
                throw new RemoteException("API returns: " + result.Code.ToString());
            }


            if (Wait)
            {
                WriteObject(WaitJobFinished(job.Id,Connection));                             
            }
            else
            {

                WriteObject(job);
            }

        }

        
    }
}
