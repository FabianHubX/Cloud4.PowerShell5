using Cloud4.CoreLibrary.Models;
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
    [Cmdlet(VerbsCommon.Get, "Cloud4User")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.User))]
    public class GetUser : BaseGetCmdLet<User, UserService>
    {
        
        [Parameter(
           Mandatory = false,
           Position = 0,
           ValueFromPipeline = true,
            HelpMessage = "Filter by e-mail Address",
           ValueFromPipelineByPropertyName = true)]

        public string eMail { get; set; }

        private UserService userService { get; set; }

        protected override void ProcessRecord()
        {


            userService = new UserService(Connection);

            try
            {
                if (string.IsNullOrEmpty(eMail))
                {
                    Task<List<Cloud4.CoreLibrary.Models.User>> callTask = Task.Run(() => userService.AllAsync());

                    callTask.Wait();
                    var users = callTask.Result;


                    users.ToList().ForEach(WriteObject);
                }
                else
                {

                    Task<Cloud4.CoreLibrary.Models.User> callTask = Task.Run(() => userService.GetAsync(eMail.ToLower()));

                    callTask.Wait();
                    var user = callTask.Result;

                    WriteObject(user);
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
