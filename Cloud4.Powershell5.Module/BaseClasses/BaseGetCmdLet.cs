using Cloud4.CoreLibrary.Models;
using Cloud4.CoreLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.Powershell5.Module
{
    public class BaseGetCmdLet<T,Y> : BaseCmdLet<T,Y> where Y : new()
    {



        public static List<T> GetAll(Connection con)
        {

            var service = Activator.CreateInstance(typeof(Y), new object[] { con });


            Task<Result<List<T>>> callTask = Task.Run(() => ((IBaseServiceInterface<T>)service).AllAsync());

            callTask.Wait();
            var result = callTask.Result;


            if (result.Object != default(List<T>))
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

        public static T GetOne(Guid Id, Connection con)
        {


            IBaseServiceInterface<T> service = (IBaseServiceInterface<T>)Activator.CreateInstance(typeof(Y), new object[] { con });


            Task<Result<T>> callTask = Task.Run(() => service.GetAsync(Id));

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
