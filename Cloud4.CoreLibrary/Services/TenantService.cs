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

        public async Task<Result<Tenant>> GetByPlatformAsync(string platformid)
        {
            var result = await client.GetDataAsJsonAsync<List<Tenant>>(new Uri(this.Connection.ApiUrl, Entity));

            Result<Tenant> returnresult = new Result<Tenant>();
            returnresult.Object = result.Content?.First(x=>x.PlatformId == platformid);
            returnresult.Code = result.StatusCode;

            return returnresult;

        }

        public async Task<string> SetCredentialsAsync(string Id, TenantCredentials body)
        {
            var result = await client.PutDataAsJsonAsync<TenantCredentials>(new Uri(this.Connection.ApiUrl, "PlatformCredentials/" + Id), body);

 
                return result.StatusCode.ToString();
        }
    }
}
