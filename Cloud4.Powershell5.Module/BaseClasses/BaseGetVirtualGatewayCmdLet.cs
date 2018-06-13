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
    public class BaseGetVirtualGatewayCmdLet<T,Y> : BaseVirtualGatewayCmdLet<T,Y> where Y : new()
    {

        

        public static List<T> GetAll(Connection con, Guid VirtualGatewayId)
        {
            try
            {

                var service = Activator.CreateInstance(typeof(Y), new object[] { con, VirtualGatewayId });


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

        public static T GetOne(Guid Id, Connection con, Guid VirtualGatewayId)
        {
            try
            {
              
                IBaseServiceInterface<T> service = (IBaseServiceInterface<T>)Activator.CreateInstance(typeof(Y), new object[] { con, VirtualGatewayId });


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
