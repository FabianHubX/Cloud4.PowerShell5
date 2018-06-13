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
    public class BaseGetLoadBalancerCmdLet<T,Y> : BaseLoadBalancerCmdLet<T,Y> where Y : new()
    {

      

        public static List<T> GetAll(Connection con, Guid VirtualLoadBalancerId)
        {
            try
            {

                var service = Activator.CreateInstance(typeof(Y), new object[] { con, VirtualLoadBalancerId });


                Task<Result<List<T>>> callTask = Task.Run(() => ((IBaseServiceInterface<T>)service).AllAsync());
            
                callTask.Wait();
                var job = callTask.Result.Object;

                return job;

            }
            catch (Exception e)
            {
                throw new RemoteException("An API Error has happen");
            }
        }

        public static T GetOne(Guid Id, Connection con, Guid VirtualLoadBalancerId)
        {
            try
            {
              
                IBaseServiceInterface<T> service = (IBaseServiceInterface<T>)Activator.CreateInstance(typeof(Y), new object[] { con, VirtualLoadBalancerId });


                Task<Result<T>> callTask = Task.Run(() => service.GetAsync(Id));

                callTask.Wait();
                var job = callTask.Result.Object;

                return job;

            }
            catch (Exception e)
            {
                throw new RemoteException("An API Error has happen");
            }
        }



    }
}
