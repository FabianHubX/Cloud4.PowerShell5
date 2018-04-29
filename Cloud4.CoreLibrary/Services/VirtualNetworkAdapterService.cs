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


        public async Task<List<VirtualNetworkAdapterService>> AllProfilesAsync()
        {
            var result = await client.GetDataAsJsonAsync<List<VirtualNetworkAdapterService>>( this.Connection.ApiUrl + "VirtualAdapterProfiles");

            return result;


        }



        public async Task<List<VirtualNetworkAdapter>> GetByvSubNetAsync(Guid vSubNetId)
        {
            var result = await client.GetDataAsJsonAsync<List<VirtualNetworkAdapter>>( this.Connection.ApiUrl + this.Connection.TenantId.ToString() + "/" + Entity + "?subnetId=" + vSubNetId.ToString());

            return result;


        }
    }
}
