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
   
    public class BaseRemoveCmdLet<T,Y> : BaseCmdLet where Y : new()
    {
        public Connection Connection { get; set; }

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
        }

    

        public static CoreLibrary.Models.Job Remove(Guid Id, Connection con, bool Wait)
        {
            try
            {
               

                var service = Activator.CreateInstance(typeof(Y), new object[] { con });
                
                Task<CoreLibrary.Models.Job> callTask = Task.Run(() => ((IBaseServiceInterface<T>)service).DeleteAsync(Id, Wait));

                callTask.Wait();
                var job = callTask.Result;

                return job;

            }
            catch (Exception e)
            {
                throw new RemoteException("An API Error has happen");
            }
        }


    }
}
