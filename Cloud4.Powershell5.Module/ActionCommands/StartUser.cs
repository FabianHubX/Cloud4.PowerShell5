using Cloud4.CoreLibrary.Services;
using Cloud4.Powershell5.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module
{
    [Cmdlet(VerbsLifecycle.Start, "Cloud4User")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class StartUser : BaseCmdLet
    {
        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = true,
          ValueFromPipelineByPropertyName = true)]

        public UserAction Action { get; set; }

        [Parameter(
           Mandatory = true,
           Position = 0,
           ValueFromPipeline = true,
           ValueFromPipelineByPropertyName = true)]

        public string Email { get; set; }

        private UserService UserService { get; set; }


        protected override void ProcessRecord()
        {


            UserService = new UserService(Connection);


            try
            {
                string email = Email.ToLower();
                string action = Action.ToString().ToLower();
                Task<bool> callTask = Task.Run(() => UserService.ActionAsync(email, new CoreLibrary.Models.ActionParameter { Action = action }));

                callTask.Wait();
                var job = callTask.Result;

                WriteObject(job);

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
