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
    public class BaseCmdLet<T,Y>: Cmdlet
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

        public T WaitJobFinished(Guid jobid, Connection con)
        {
            JobService jobService = new JobService(con);

            Task<CoreLibrary.Models.Result<CoreLibrary.Models.Job>> callTask;
            CoreLibrary.Models.Job job;

            do
            {
                callTask = Task.Run(() => jobService.GetAsync(jobid));

                callTask.Wait();
                job = callTask.Result.Object;
                
                Thread.Sleep(new TimeSpan(0, 0, 5));              

                Console.WriteLine("Wait until Job Finished...");
            }
            while (job.State != "failed" && job.State != "succeeded");



            var service = Activator.CreateInstance(typeof(Y), new object[] { con });

            Task<Result<T>> callTasklist = Task.Run(() => ((IBaseServiceInterface<T>)service).GetAsync(job.ResourceId));

            callTasklist.Wait();

            if (callTasklist.Result.Object != null)
            {
                return callTasklist.Result.Object;
            }
            else if (callTasklist.Result.Error != null)
            {
                throw new RemoteException("Conflict Error: " + callTasklist.Result.Error.ErrorType + "\r\n" + callTasklist.Result.Error.FaultyValues);
            }
            else
            {
                throw new RemoteException("API returns: " + callTasklist.Result.Code.ToString());
            }

        }


        public static T Get(Connection con, Guid Id)
        {
            var service = Activator.CreateInstance(typeof(Y), new object[] { con });

            Task<Result<T>> callTasksubNet = Task.Run(() => ((IBaseServiceInterface<T>)service).GetAsync(Id));

            callTasksubNet.Wait();

            if (callTasksubNet.Result.Job != null)
            {
                return callTasksubNet.Result.Object;
            }
            else if (callTasksubNet.Result.Error != null)
            {
                throw new RemoteException("Conflict Error: " + callTasksubNet.Result.Error.ErrorType + "\r\n" + callTasksubNet.Result.Error.FaultyValues);
            }
            else
            {
                throw new RemoteException("API returns: " + callTasksubNet.Result.Code.ToString());
            }


        }

        public static Z GetSpecial<Z, S>(Connection con, Guid Id)
        {
            var service = Activator.CreateInstance(typeof(S), new object[] { con });

            Task<Result<Z>> callTasksubNet = Task.Run(() => ((IBaseServiceInterface<Z>)service).GetAsync(Id));

            callTasksubNet.Wait();

            if (callTasksubNet.Result.Job != null)
            {
                return callTasksubNet.Result.Object;
            }
            else if (callTasksubNet.Result.Error != null)
            {
                throw new RemoteException("Conflict Error: " + callTasksubNet.Result.Error.ErrorType + "\r\n" + callTasksubNet.Result.Error.FaultyValues);
            }
            else
            {
                throw new RemoteException("API returns: " + callTasksubNet.Result.Code.ToString());
            }


        }
    }
}
