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
    public class BaseCmdLet: Cmdlet
    {
        public Connection Connection { get; set; }

        private CompanyService checkcon;

        protected override void BeginProcessing()
        {
            Guid runspId;
            var runsp = PowerShell.Create(RunspaceMode.CurrentRunspace);
            runspId = runsp.Runspace.InstanceId;
            Connection = (Connection)TokenCollection.Get(runspId);

   
            if (Connection == null)
            {
                throw new ArgumentException("No Active Connection");
            }

            if (string.IsNullOrEmpty(Connection.AccessToken))
            {
                throw new ArgumentException("No Valid Connection");
            }

            checkcon = new CompanyService(Connection);
            checkcon.OnRefreshConnectionRaised += Service_OnRefreshConnectionRaised;
            var currentcompany = Task.Run(() => checkcon.GetCurrentAsync()).Result;


        }

        private void Service_OnRefreshConnectionRaised()
        {
            using (var runsp = PowerShell.Create(RunspaceMode.CurrentRunspace))
            {
                var runspId = runsp.Runspace.InstanceId;


                TokenCollection.Replace(runspId, checkcon.Connection);

            }

            WriteObject(checkcon.Connection);
        }

        public void WaitJobFinished(Guid jobid)
        {
            JobService jobService = new JobService(Connection);

            Task<CoreLibrary.Models.Job> callTask;
            CoreLibrary.Models.Job job;

            do
            {
                callTask = Task.Run(() => jobService.GetAsync(jobid));

                callTask.Wait();
                job = callTask.Result;
                
                Thread.Sleep(new TimeSpan(0, 0, 5));              

                Console.WriteLine("Wait until Job Finished...");
            }
            while (job.State != "failed" && job.State != "succeeded");
        }
    }
}
