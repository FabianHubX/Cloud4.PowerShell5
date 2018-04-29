using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class UserService : BaseService<User, User, User>
    {
        public UserService()
        {

        }
        public UserService(Connection con) : base(con)
        {
            this.Entity = "Users";
    
        }


        public async Task<bool> ActionAsync(string email, ActionParameter body)
        {
            DataClientResult result = await client.PostDataAsJsonAsync<ActionParameter>( this.Connection.ApiUrl + Entity + "/" +email + "/actions", body);

            return result.StatusCode == System.Net.HttpStatusCode.Accepted;
        }

        public async Task<User> GetAsync(string email)
        {
            var result = await client.GetDataAsJsonAsync<User>( this.Connection.ApiUrl + Entity + "/" + email);

            return result;


        }
    }
}


