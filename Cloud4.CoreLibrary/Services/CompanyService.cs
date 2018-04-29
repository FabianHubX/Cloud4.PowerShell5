using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class CompanyService : BaseService<Company, Company, Company>
    {
        public CompanyService()
        {

        }

        public CompanyService(Connection con) : base(con)
        {
            this.Entity = "Companies";
        
        }

        

        public async Task<Company> GetCurrentAsync()
        {
            var result = await client.GetDataAsJsonAsync<Company>(this.Connection.ApiUrl + Entity + "/current");

            return result;


        }
    }
}


