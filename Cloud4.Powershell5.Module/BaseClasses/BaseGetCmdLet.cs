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
    public class BaseGetCmdLet<T,Y> : BaseCmdLet where Y : new()
    {



        public static List<T> GetAll(Connection con)
        {
            try
            {

                var service = Activator.CreateInstance(typeof(Y), new object[] { con });


                Task<List<T>> callTask = Task.Run(() => ((IBaseServiceInterface<T>)service).AllAsync());
            
                callTask.Wait();
                var job = callTask.Result;

                return job;

            }
            catch (Exception e)
            {
                throw new RemoteException("An API Error has happen");
            }
        }

        public static T GetOne(Guid Id, Connection con)
        {
            try
            {
              
                IBaseServiceInterface<T> service = (IBaseServiceInterface<T>)Activator.CreateInstance(typeof(Y), new object[] { con });


                Task<T> callTask = Task.Run(() => service.GetAsync(Id));

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
