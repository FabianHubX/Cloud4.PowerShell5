using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class VirtualSubNetService : BaseTenantService<VirtualSubNet, CreateVirtualSubNet, VirtualSubNet>
    {
        public VirtualSubNetService()
        {

        }
        public VirtualSubNetService(Connection con) : base(con)
        {
            this.Entity = "VirtualSubnets";
        
        }



        public async Task<List<VirtualSubNet>> GetByvNetAsync(Guid vNetId)
        {
            var result = await client.GetDataAsJsonAsync<List<VirtualSubNet>>( this.Connection.ApiUrl + this.Connection.TenantId.ToString() + "/" + Entity + "?vnetId=" + vNetId.ToString());

            return result;


        }
    }
}
