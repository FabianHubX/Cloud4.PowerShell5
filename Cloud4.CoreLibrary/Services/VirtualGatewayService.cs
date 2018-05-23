using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class VirtualGatewayService : BaseTenantService<VirtualGateway, CreateVirtualGateway, VirtualGateway>
    {
        public VirtualGatewayService()
        {

        }
        public VirtualGatewayService(Connection con) : base(con)
        {
            this.Entity = "VitualGateways";
           
        }



        public async Task<Result<List<VirtualGateway>>> GetByvDCAsync(Guid vDcId)
        {
            var result = await client.GetDataAsJsonAsync<List<VirtualGateway>>(new Uri(this.Connection.ApiUrl, this.Connection.TenantId.ToString() + "/" + Entity + "?vdcId=" + vDcId.ToString()));

            Result<List<VirtualGateway>> returnresult = new Result<List<VirtualGateway>>();
            returnresult.Object = result.Content;
            returnresult.Code = result.StatusCode;

            return returnresult;
        }






    }
}
