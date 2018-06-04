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
    [Cmdlet(VerbsData.Update, "Cloud4VM")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class UpdateVirtualMachine : BaseTenantUpdateCmdLet<VirtualMachine, VirtualMachineService, VirtualMachine>
    {
        [Parameter(
     Mandatory = true,
     Position = 0,
     ValueFromPipeline = true,
      HelpMessage = "Filter by Virutal Machine Id",
     ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }

        [Parameter(
     Mandatory = false,
     Position = 1,
     ValueFromPipeline = true,
       HelpMessage = "Profile of the virtual Maschine",
     ValueFromPipelineByPropertyName = true)]

        public string VMProfile { get; set; }

        [Parameter(
Mandatory = false,
Position = 2,
ValueFromPipeline = true,
HelpMessage = "Enable Remote Access for the virtual Maschine",
ValueFromPipelineByPropertyName = true)]

        public bool? EnableRemoteAccess { get; set; }

        [Parameter(
Mandatory = false,
Position = 3,
ValueFromPipeline = true,
HelpMessage = "Enable Internet Access for the virtual Maschine",
ValueFromPipelineByPropertyName = true)]

        public bool? EnableInternetAccess { get; set; }



        [Parameter(
      Mandatory = false,
      Position = 4,
      ValueFromPipeline = true,
       HelpMessage = "Wait Job Finished",
      ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }




        protected override void ProcessRecord()
        {

            var vm = Get(Connection, Id);

            if (!string.IsNullOrEmpty(VMProfile))
            {
                vm.VirtualMachineProfile = VMProfile;

                var job = Update(Connection, Id, vm);

                if (Wait)
                {
                    WriteObject(WaitJobFinished(job.Id, Connection));

                }
                else
                {
                    WriteObject(job);
                }

            }

            VirtualMachineService service = new VirtualMachineService(Connection);

            if (EnableRemoteAccess.HasValue)
            {
                string action = "";
                if (EnableRemoteAccess.Value)
                {
                    action = "EnableRemoteAccess";
                }
                else
                {
                    action = "DisableRemoteAccess";
                }

            
                Task<Cloud4.CoreLibrary.Models.Job> callTask = Task.Run(() => service.ActionAsync(Id, new CoreLibrary.Models.ActionParameter { Action = action }));

                callTask.Wait();
                var job = callTask.Result;
                if (Wait)
                {
                    WriteObject(WaitJobFinished(job.Id, Connection));
                }
                else
                {
                    WriteObject(job);
                }

            }
            if (EnableInternetAccess.HasValue)
            {
                string action = "";
                if (EnableRemoteAccess.Value)
                {
                    action = "EnableInternetAccess";
                }
                else
                {
                    action = "DisableInternetAccess";
                }

                Task<Cloud4.CoreLibrary.Models.Job> callTask = Task.Run(() => service.ActionAsync(Id, new CoreLibrary.Models.ActionParameter { Action = action }));

                callTask.Wait();
                var job = callTask.Result;
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
