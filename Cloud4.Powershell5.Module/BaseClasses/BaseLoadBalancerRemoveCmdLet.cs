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
   
    public class BaseLoadBalancerRemoveCmdLet<T,Y> : BaseLoadBalancerCmdLet<T,Y> where Y : new()
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

    

        public static CoreLibrary.Models.Job Remove(Guid Id, Connection con, Guid VirtualLoadBalancerId, bool Wait)
        {
           
                var service = Activator.CreateInstance(typeof(Y), new object[] { con, VirtualLoadBalancerId });
                
                Task<CoreLibrary.Models.Result> callTask = Task.Run(() => ((IBaseServiceInterface<T>)service).DeleteAsync(Id, Wait));

                callTask.Wait();
                var result = callTask.Result;

                if (result.Job != null)
                {
                    return result.Job;
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
