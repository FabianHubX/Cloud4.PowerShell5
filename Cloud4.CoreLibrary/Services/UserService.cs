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

        public async Task<Result<User>> GetAsync(string email)
        {
            var result = await client.GetDataAsJsonAsync<User>(new Uri(this.Connection.ApiUrl, Entity + "/" + email));

            Result<User> returnresult = new Result<User>();
            returnresult.Object = result.Content;
            returnresult.Code = result.StatusCode;

            return returnresult;

        }
    }
}


