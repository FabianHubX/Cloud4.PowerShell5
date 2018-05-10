using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class VirtualLoadBalancerService : BaseTenantService<VirtualLoadBalancer, CreateVirtualLoadBalancer, VirtualLoadBalancer>
    {
        public VirtualLoadBalancerService()
        {

        }
        public VirtualLoadBalancerService(Connection con) : base(con)
        {
            this.Entity = "LoadBalancers";
           
        }



        public async Task<Result<List<VirtualLoadBalancer>>> GetByvDCAsync(Guid vDcId)
        {
            var result = await client.GetDataAsJsonAsync<List<VirtualLoadBalancer>>(new Uri(this.Connection.ApiUrl, this.Connection.TenantId.ToString() + "/" + Entity + "?vdcId=" + vDcId.ToString()));

            Result<List<VirtualLoadBalancer>> returnresult = new Result<List<VirtualLoadBalancer>>();
            returnresult.Object = result.Content;
            returnresult.Code = result.StatusCode;

            return returnresult;
        }






    }
}
