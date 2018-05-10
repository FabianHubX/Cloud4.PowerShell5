using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class VirtualNetworkAdapterService : BaseTenantService<VirtualNetworkAdapter, CreateVirtualNetworkAdapter, CreateVirtualNetworkAdapter>
    {
        public VirtualNetworkAdapterService()
        {

        }

        public VirtualNetworkAdapterService(Connection con) : base(con)
        {
            this.Entity = "VirtualNetworkAdapters";
         
        }


        public async Task<Result<List<VirtualNetworkAdapterProfile>>> AllProfilesAsync()
        {
            var result = await client.GetDataAsJsonAsync<List<VirtualNetworkAdapterProfile>>(new Uri(this.Connection.ApiUrl, "VirtualAdapterProfiles"));

            Result<List<VirtualNetworkAdapterProfile>> returnresult = new Result<List<VirtualNetworkAdapterProfile>>();
            returnresult.Object = result.Content;
            returnresult.Code = result.StatusCode;

            return returnresult;
        }



        public async Task<Result<List<VirtualNetworkAdapter>>> GetByvSubNetAsync(Guid vSubNetId)
        {
            var result = await client.GetDataAsJsonAsync<List<VirtualNetworkAdapter>>(new Uri(this.Connection.ApiUrl, this.Connection.TenantId.ToString() + "/" + Entity + "?subnetId=" + vSubNetId.ToString()));

            Result<List<VirtualNetworkAdapter>> returnresult = new Result<List<VirtualNetworkAdapter>>();
            returnresult.Object = result.Content;
            returnresult.Code = result.StatusCode;

            return returnresult;

        }
    }
}
