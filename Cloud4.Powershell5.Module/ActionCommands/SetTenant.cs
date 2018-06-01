using Cloud4.CoreLibrary.Models;
using Cloud4.CoreLibrary.Services;
using Cloud4.Powershell5.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module
{

    [Cmdlet(VerbsCommon.Set, "Cloud4Tenant")]
    [OutputType(typeof(ConnectionResult))]
    public class SetTenant : Cmdlet
    {
        private Uri _apiUrl;
        private Uri _loginUrl;

        private Guid PlatformId = new Guid("13d91c81-a9a7-4585-b653-a54ea363f763");


        private Tenant tenant { get; set; }

    

        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]

        public Guid Id { get; set; }




        protected override void ProcessRecord()
        {
            Guid runspId;
            var runsp = PowerShell.Create(RunspaceMode.CurrentRunspace);
            runspId = runsp.Runspace.InstanceId;
            var con = (Connection)TokenCollection.Get(runspId);


            if (con == null)
            {
                throw new ArgumentException("No Active Connection");
            }

            if (string.IsNullOrEmpty(con.AccessToken))
            {
                throw new ArgumentException("No Valid Connection");
            }

            TenantService tenantService = new TenantService(con);

         


            try
            {

                Task<Result<List<Tenant>>> callTask = Task.Run(() => tenantService.GetByPlatformAsync(PlatformId));

                callTask.Wait();

                if (callTask.Result.Object != null)
                {
                    if (callTask.Result.Object.Count == 0)
                    {
                        Console.WriteLine("No Tenant existing");
                    }
                    else if (callTask.Result.Object.Count == 1)
                    {
                        tenant = callTask.Result.Object.First();
                        con.TenantId = tenant.Id;


                        WriteObject(new ConnectionResult { UserName = con.UserName, ApiUrl = con.ApiUrl, LogonUrl = con.LogonUrl, TenantId = con.TenantId });
                    }
                    else
                    {

                        if (callTask.Result.Object.Any(x => x.Id == Id))
                        {
                            tenant = callTask.Result.Object.First(x => x.Id == Id);
                            con.TenantId = tenant.Id;


                            WriteObject(new ConnectionResult { UserName = con.UserName, ApiUrl = con.ApiUrl, LogonUrl = con.LogonUrl, TenantId = con.TenantId });
                        }
                        else
                        {
                            Console.WriteLine("As multiple Tenant exists please Set Tenant focus.");

                            WriteObject(callTask.Result.Object);
                        }


                    }

                 
                    using (runsp = PowerShell.Create(RunspaceMode.CurrentRunspace))
                    {
                        runspId = runsp.Runspace.InstanceId;
                        TokenCollection.Replace(runspId, con);

                    }
                }
                else if (callTask.Result.Error != null)
                {
                    throw new RemoteException("Conflict Error: " + callTask.Result.Error.ErrorType + "\r\n" + callTask.Result.Error.FaultyValues);
                }
                else
                {
                    throw new RemoteException("API returns: " + callTask.Result.Code.ToString());
                }

            
             
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }




        }

        protected override void EndProcessing()
        {
            {
                if (string.IsNullOrEmpty(TokenService.AccessToken))
                {
                    Console.WriteLine("Connection failed, User Name or Password wrong.");
                }
                else
                {
                    Console.WriteLine("Connection established");
                }

                //if (Id == Guid.Empty)
                //{

                //    Console.WriteLine("To Connect you need to select a platform:");
                //}
            }
        }
    }
}

