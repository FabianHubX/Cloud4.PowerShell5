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
    [OutputType(typeof(Connection))]
    public class OpenConnection : Cmdlet
    {
        private Uri _apiUrl;
        private Uri _loginUrl;

        


        private Tenant tenant { get; set; }

        private List<Platform> platforms { get; set; }



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

        public Guid Id { get; set; }



        protected override void BeginProcessing()
        {
           
        }

        protected override void ProcessRecord()
        {

            string password = new System.Net.NetworkCredential(string.Empty, Credential.Password).Password;


            TokenService.Connect(_loginUrl, Credential.UserName, password);

            var con = new Connection { UserName = Credential.UserName, PassWord = password, LogonUrl = _loginUrl, AccessToken = TokenService.AccessToken, ApiUrl = _apiUrl};



            if (Id == Guid.Empty)
            {
                // No Platform selected
                PlatformService platformService = new PlatformService(con);

                try
                {

                    Task<List<Platform>> callTask = Task.Run(() => platformService.AllAsync());

                    callTask.Wait();
                    platforms = callTask.Result;

                    platforms.ToList().ForEach(WriteObject);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            else
            {
                TenantService tenantService = new TenantService(con);
                JobService jobService = new JobService(con);

                try
                {

                    Task<Tenant> callTask = Task.Run(() => tenantService.GetByPlatformAsync(Id.ToString("D").ToLower()));

                    callTask.Wait();
                    tenant = callTask.Result;

                    if (tenant == null)
                    {
                        Console.WriteLine("No Tenant existing");
                        CoreLibrary.Models.Job job;


                        Task<CoreLibrary.Models.Job> callTaskJob = Task.Run(() => tenantService.CreateAsync(new Tenant { Name = "Swiss Cloud 4.0", PlatformId = Id.ToString("D").ToLower() }));

                        job = callTaskJob.Result;
                 

                        Console.WriteLine("Create new Tenant, Job: " + job.Id.ToString());

                        do
                        {
                            callTaskJob = Task.Run(() => jobService.GetAsync(job.Id));

                            callTaskJob.Wait();
                            job = callTaskJob.Result;

                            Console.WriteLine("Job tatus: " + job.State);

                            Thread.Sleep(new TimeSpan(0, 0, 5));

                            Console.WriteLine("Wait...");
                        }
                        while (job.State == "failed" || job.State == "successful");


                        callTask = Task.Run(() => tenantService.GetByPlatformAsync(Id.ToString("D").ToLower()));

                        callTask.Wait();
                        tenant = callTask.Result;

                        Console.WriteLine("Get Tenant id");
                    }


                    Console.WriteLine("TenantId: " + tenant.Id);

                    con.TenantId = tenant.Id;

                    Guid runspId;
                    using (var runsp = PowerShell.Create(RunspaceMode.CurrentRunspace))
                    {
                        runspId = runsp.Runspace.InstanceId;
                        TokenCollection.Replace(runspId, con);

                    }

                    WriteObject(con);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }




            }
        }

        protected override void EndProcessing()
        {
            if (string.IsNullOrEmpty(TokenService.AccessToken))
            {
                Console.WriteLine("Connection failed, User Name or Password wrong.");
            }
            else
            {
                Console.WriteLine("Connection established");
            }

            if (Id == Guid.Empty)
            {

                Console.WriteLine("To Connect you need to select a platform:");
            }
        }
    }
}
