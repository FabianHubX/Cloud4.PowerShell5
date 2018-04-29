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

        public async Task<Tenant> GetByPlatformAsync(string platformid)
        {
            var result = await client.GetDataAsJsonAsync<List<Tenant>>( this.Connection.ApiUrl + Entity);

            return result?.First(x => x.PlatformId == platformid);

        }

        public async Task<string> SetCredentialsAsync(string Id, TenantCredentials body)
        {
            var result = await client.PutDataAsJsonAsync<TenantCredentials>( this.Connection.ApiUrl + "PlatformCredentials/" + Id, body);

 
                return result.StatusCode.ToString();
        }
    }
}
