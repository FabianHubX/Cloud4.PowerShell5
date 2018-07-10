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
    [Cmdlet(VerbsCommon.Set, "Cloud4vDisk")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class SetVirtualDisk: BaseTenantUpdateCmdLet<VirtualDisk, VirtualSubNetService, UpdateVirtualDisk>
    {
        [Parameter(
     Mandatory = true,
     Position = 0,
     ValueFromPipeline = true,
      HelpMessage = "Filter by vDisk Id",
     ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
     Mandatory = false,
     Position = 1,
     ValueFromPipeline = true,
       HelpMessage = "Name of the virtual Disk",
     ValueFromPipelineByPropertyName = true)]

        public string Name { get; set; }


        [Parameter(
     Mandatory = false,
     Position = 2,
     ValueFromPipeline = true,
       HelpMessage = "Profile vDisk",
     ValueFromPipelineByPropertyName = true)]

        public string VirtualDiskProfile { get; set; }


        [Parameter(
      Mandatory = false,
      Position = 3,
      ValueFromPipeline = true,
       HelpMessage = "Wait Job Finished",
      ValueFromPipelineByPropertyName = true)]

        public SwitchParameter Wait { get; set; }




        protected override void ProcessRecord()
        {

            var vdisk = Get(Connection, Id);

            var newvdisk = new UpdateVirtualDisk
            {

                Name = vdisk.Name,
                 VirtualDiskProfileName = vdisk.VirtualDiskProfileName
            };

            bool IsChanged = false;

            if (!string.IsNullOrEmpty(Name))
            {
                newvdisk.Name = Name;
                IsChanged = true;
            }


            if (!string.IsNullOrEmpty(VirtualDiskProfile))
            {
                newvdisk.VirtualDiskProfileName = VirtualDiskProfile;
                IsChanged = true;
            }


            if (IsChanged)
            {

                var job = Update(Connection, Id, newvdisk);

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
}
