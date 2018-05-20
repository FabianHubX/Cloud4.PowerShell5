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
    [Cmdlet(VerbsCommon.Set, "Cloud4TenantCredentials")]
    [OutputType(typeof(Cloud4.CoreLibrary.Models.Job))]
    public class SetTenantCredentials : BaseActionCmdLet<Tenant, TenantService>
    {

     

        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipelineByPropertyName = true)]
      
        public Guid TenantId { get; set; }

        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipelineByPropertyName = true)]
        public string Url { get; set; }

        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipelineByPropertyName = true)]
        public string UserName { get; set; }

        [Parameter(
           Mandatory = true,
           Position = 1,
           ValueFromPipelineByPropertyName = true)]
        public string Password { get; set; }

        private TenantService Service;


        protected override void ProcessRecord()
        {


            Service = new TenantService(Connection);

         
            Task<string> callTask = Task.Run(() => Service.SetCredentialsAsync(TenantId, new TenantCredentials { Url = Url, Username = UserName, Password = Password }));

            callTask.Wait();
            var job = callTask.Result;


            WriteObject(job);


        }

        
      

    }
}
