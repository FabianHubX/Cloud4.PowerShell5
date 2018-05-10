// Copyright (c) HIAG Data AG. All Rights Reserved. Licensed under the GNU License.  See License.txt
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
    public class VirtualMachineService : BaseTenantService<VirtualMachine,CreateVirtualMachine, VirtualMachine>
    {
        public VirtualMachineService()
        {

        }

        public VirtualMachineService(Connection con) : base(con)
        {
            this.Entity = "VirtualMachines";
          
        }

      

        public async Task<Job> ActionAsync(Guid Id, ActionParameter body)
        {
            DataClientResult result = await client.PostDataAsJsonAsync<ActionParameter>( this.Connection.ApiUrl + this.Connection.TenantId.ToString() + "/" + Entity + "/" + Id.ToString() + "/actions", body);

            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var job = JsonConvert.DeserializeObject<Job>(result.Content);
                return job;
            }
            else
            {
                return null;
            }
        }


        public async Task<Result<List<VirtualMachine>>> GetByvDCAsync(Guid vDcId)
        {
            var result = await client.GetDataAsJsonAsync<List<VirtualMachine>>(new Uri(this.Connection.ApiUrl, this.Connection.TenantId.ToString() + "/" + Entity + "?vdcId=" + vDcId.ToString()));

            Result<List<VirtualMachine>> returnresult = new Result<List<VirtualMachine>>();
            returnresult.Object = result.Content;
            returnresult.Code = result.StatusCode;

            return returnresult;

        }
    }
}
