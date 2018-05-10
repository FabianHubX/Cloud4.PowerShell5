// Copyright (c) HIAG Data AG. All Rights Reserved. Licensed under the GNU License.  See License.txt
using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class VirtualNetworkService : BaseTenantService<VirtualNetwork, CreateVirtualNetwork, VirtualNetwork>
    {
        public VirtualNetworkService()
        {

        }
     
        public VirtualNetworkService(Connection con) : base(con)
        {
            this.Entity = "VirtualNetworks";
          
        }



        public async Task<Result<List<VirtualNetwork>>> GetByvDCAsync(Guid vDcId)
        {
            var result = await client.GetDataAsJsonAsync<List<VirtualNetwork>>(new Uri(this.Connection.ApiUrl, this.Connection.TenantId.ToString() + "/" + Entity + "?vdcId=" + vDcId.ToString()));

            Result<List<VirtualNetwork>> returnresult = new Result<List<VirtualNetwork>>();
            returnresult.Object = result.Content;
            returnresult.Code = result.StatusCode;

            return returnresult;


        }
    }
}
