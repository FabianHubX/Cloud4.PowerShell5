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
    [Cmdlet(VerbsLifecycle.Stop, "Cloud4VM")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class StopVirtualMachine : BaseCmdLet
    {
     


        [Parameter(
           Mandatory = true,
           Position = 0,       
           ValueFromPipelineByPropertyName = true)]
      
        public Guid Id { get; set; }



        [Parameter(
           Mandatory = false,
           Position = 1,
           ValueFromPipelineByPropertyName = true)]

        public bool ForcePowerOff { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 2,
            ValueFromPipeline = true,
            HelpMessage = "Wait Job Finished",
            ValueFromPipelineByPropertyName = true)]

        public bool Wait { get; set; }

        private VirtualMachineService Service { get; set; }


        protected override void ProcessRecord()
        {

            Service = new VirtualMachineService(Connection);

            string action = "";

            if (ForcePowerOff)
            {
                action = "PowerOff";
            }
            else
            {
                action = "Shutdown";
            }

            try
            {
                string vmid = Id.ToString("D").ToLower();
                Task<Cloud4.CoreLibrary.Models.Job> callTask = Task.Run(() => Service.ActionAsync(vmid, new CoreLibrary.Models.ActionParameter { Action = action }));

                callTask.Wait();
                var job = callTask.Result;
                if (Wait)
                {
                    WaitJobFinished(job.Id);
                    Task<VirtualMachine> callTasklist = Task.Run(() => Service.GetAsync(job.ResourceId));

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
