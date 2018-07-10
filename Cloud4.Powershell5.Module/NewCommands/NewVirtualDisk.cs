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
    [Cmdlet(VerbsCommon.New, "Cloud4vDisk")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class NewVirtualDisk : BaseTenantNewCmdLet<VirtualDisk, VirtualDiskService, CreateVirtualDisk>
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

    


        protected override void ProcessRecord()
        {


    

            var virtualDisk = new CreateVirtualDisk
            {
                Name = _name,
                VirtualDiskProfileName = _diskProfile,
                virtualMachineId = VirtualMachineId
            };



            var job = Create(Connection, virtualDisk);


            if (Wait)
            {
                WriteObject(WaitJobFinished(job.Id, Connection));
            }
            else
            {
                WriteObject(job);
            }
        }

        
    }
}
