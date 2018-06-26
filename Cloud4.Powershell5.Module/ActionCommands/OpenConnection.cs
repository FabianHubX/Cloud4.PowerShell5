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

    [Cmdlet(VerbsCommon.Open, "Cloud4Connection")]
    [OutputType(typeof(ConnectionResult))]
    public class OpenConnection : Cmdlet
    {
        private Uri _apiUrl;
        private Uri _loginUrl;

        private Guid PlatformId = new Guid("13d91c81-a9a7-4585-b653-a54ea363f763");


        private Tenant tenant { get; set; }

      //  private List<Platform> platforms { get; set; }



        [Parameter(
          Mandatory = true,
          Position = 0,
          ValueFromPipeline = false,
          ValueFromPipelineByPropertyName = false)]

        public string LoginUrl
        {
            get
            {
                return _loginUrl.AbsolutePath;

            }
            set
            {
                _loginUrl = new Uri(value);
            }
        }

        [Parameter(
       Mandatory = true,
       Position = 1,
       ValueFromPipeline = false,
       ValueFromPipelineByPropertyName = false)]

        public string ApiUrl
        {
            get
            {
                return _apiUrl.AbsolutePath;

            }
            set
            {
                string apiurl = value;
                apiurl += "/api/v1/";
                _apiUrl = new Uri(apiurl);
            }
        }


        [Parameter(
         Mandatory = true,
         Position = 2,
         ValueFromPipeline = true,
         ValueFromPipelineByPropertyName = true)]

        public PSCredential Credential { get; set; }


        [Parameter(
            Mandatory = false,
            Position = 3,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]

        public Guid TenantId { get; set; }




        protected override void ProcessRecord()
        {

            string password = new System.Net.NetworkCredential(string.Empty, Credential.Password).Password;

            TokenService.Connect(_loginUrl, Credential.UserName, password);

            var con = new Connection { UserName = Credential.UserName, PassWord = password, LogonUrl = _loginUrl, AccessToken = TokenService.AccessToken, ApiUrl = _apiUrl, ExpiresAt = TokenService.ExpiresAt, RefreshToken = TokenService.RefreshToken };



            //if (Id == Guid.Empty)
            //{
            //    // No Platform selected
            //    PlatformService platformService = new PlatformService(con);

            //    try
            //    {

            //        Task<Result<List<Platform>>> callTask = Task.Run(() => platformService.AllAsync());

            //        callTask.Wait();
            //        platforms = callTask.Result.Object;

            //        platforms.ToList().ForEach(WriteObject);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine("Error: " + e.Message);
            //    }
            //}
            //else
            //{
            TenantService tenantService = new TenantService(con);
            JobService jobService = new JobService(con);

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

                        if (TenantId != Guid.Empty & callTask.Result.Object.Any(x => x.Id == TenantId))
                        {
                            tenant = callTask.Result.Object.First(x => x.Id == TenantId);
                            con.TenantId = tenant.Id;


                            WriteObject(new ConnectionResult { UserName = con.UserName, ApiUrl = con.ApiUrl, LogonUrl = con.LogonUrl, TenantId = con.TenantId });
                        }
                        else
                        {
                            Console.WriteLine("As multiple Tenant exists please Set Tenant focus.");

                            WriteObject(callTask.Result.Object);
                        }


                    }

                    Guid runspId;
                    using (var runsp = PowerShell.Create(RunspaceMode.CurrentRunspace))
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

