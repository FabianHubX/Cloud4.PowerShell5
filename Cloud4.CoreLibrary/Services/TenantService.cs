// Copyright (c) HIAG Data AG. All Rights Reserved. Licensed under the GNU License.  See License.txt
using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class TenantService : BaseService<Tenant, Tenant, Tenant>
    { 
        public TenantService() 
        {

        }
  
        public TenantService(Connection con) : base(con)
        {
            
            this.Entity = "Tenants";
          

        }

        public async Task<Result<List<Tenant>>> GetByPlatformAsync(Guid platformid)
        {
            var result = await client.GetDataAsJsonAsync<List<Tenant>>(new Uri(this.Connection.ApiUrl, Entity));

            Result<List<Tenant>> returnresult = new Result<List<Tenant>>();
            returnresult.Object = result.Content.Where(x=>x.PlatformId == platformid).ToList();
            returnresult.Code = result.StatusCode;

            return returnresult;

        }

        public async Task<string> SetCredentialsAsync(Guid Id, TenantCredentials body)
        {
            var result = await client.PutDataAsJsonAsync<TenantCredentials>(new Uri(this.Connection.ApiUrl, "PlatformCredentials/" + Id.ToString()), body);

 
                return result.StatusCode.ToString();
        }
    }
}
