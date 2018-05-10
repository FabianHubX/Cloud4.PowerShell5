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
    [Cmdlet(VerbsLifecycle.Start, "Cloud4VM")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class StartVirtualMachine : BaseActionCmdLet<VirtualMachine,VirtualMachineService>
    {

     

        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipelineByPropertyName = true)]
      
        public Guid Id { get; set; }


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

           
            Task<Cloud4.CoreLibrary.Models.Job> callTask = Task.Run(() => Service.ActionAsync(Id, new CoreLibrary.Models.ActionParameter { Action = "start" }));

            callTask.Wait();
            var job = callTask.Result;
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
