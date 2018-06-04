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
    public class BaseTenantUpdateCmdLet<T, Y, U> : BaseTenantCmdLet<T, Y> where Y : new()
    {
        public static CoreLibrary.Models.Job Update(Connection con, Guid Id, U toupdateobject)
        {
            var service = Activator.CreateInstance(typeof(Y), new object[] { con });

            Task<CoreLibrary.Models.Result> callTask = Task.Run(() => ((IBaseUpdateServiceInterface<U>)service).UpdateAsync(Id, toupdateobject));

            callTask.Wait();
            var result = callTask.Result;
            CoreLibrary.Models.Job job;

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
