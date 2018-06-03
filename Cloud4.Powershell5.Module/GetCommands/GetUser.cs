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

        protected override void ProcessRecord()
        {

            if (string.IsNullOrEmpty(eMail))
            {
                GetAll(Connection).ForEach(WriteObject);
            }
            else
            {
                WriteObject(GetOnebyEMail(eMail, Connection));
            }

          
        }

        
        

        public static User GetOnebyEMail(string email, Connection con)
        {

            UserService service = new UserService(con);
           
            Task<Result<User>> callTask = Task.Run(() => service.GetAsync(email.ToLower()));

            callTask.Wait();
            var result = callTask.Result;


            if (result.Object != null)
            {
                return result.Object;
            }
            else if (result.Error != null)
            {
                throw new RemoteException("Conflict Error: " + result.Error.ErrorType + "\r\n" + result.Error.FaultyValues);
            }
            else
            {
                throw new RemoteException("API returns: " + result.Code.ToString());
            }

        }

    }
}
