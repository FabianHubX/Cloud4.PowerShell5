using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Cloud4.CoreLibrary.Services
{
    public class BaseTenantService<T,Y,Z> : IBaseServiceInterface<T>, IBaseCreateServiceInterface<Y>, IBaseUpdateServiceInterface<Z>
    {
        protected Connection Connection { get; set; }

        protected ResourceDataClient client { get; set; }

        public delegate void OnRefreshConnection();
        public event OnRefreshConnection OnRefreshConnectionRaised;

        protected string Entity { get; set; }

        public BaseTenantService()
        {

        }
        public BaseTenantService(Connection con)
        {
            client = new ResourceDataClient();
            client.Connection = con;
            this.Connection = con;

            client.OnRefreshTokenRaised += Client_OnRefreshTokenRaised;
        }

        private void Client_OnRefreshTokenRaised()
        {
            this.Connection.AccessToken = client.Connection.AccessToken;

            if (OnRefreshConnectionRaised != null)
            {
                OnRefreshConnectionRaised();
            }
        }

        public virtual async Task<List<T>> AllAsync()
        {
            var result = await client.GetDataAsJsonAsync<List<T>>( this.Connection.ApiUrl + this.Connection.TenantId.ToString() + "/" + Entity);

            return result;


        }

        public virtual async Task<T> GetAsync(Guid Id)
        {
            var result = await client.GetDataAsJsonAsync<T>( this.Connection.ApiUrl + this.Connection.TenantId.ToString() + "/" + Entity + "/" + Id.ToString());

            return result;


        }

        public virtual async Task<Job> CreateAsync(Y body)
        {
            DataClientResult result = await client.PostDataAsJsonAsync<Y>( this.Connection.ApiUrl + this.Connection.TenantId.ToString() + "/" + Entity, body);

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

        public virtual async Task<Job> UpdateAsync(Guid Id, Z body)
        {
            DataClientResult result = await client.PutDataAsJsonAsync<Z>( this.Connection.ApiUrl + this.Connection.TenantId.ToString() + "/" + Entity + "/" + Id.ToString(), body);

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

        public virtual async Task<Job> DeleteAsync(Guid Id, bool Wait)
        {
            DataClientResult result = await client.DeleteDataAsJsonAsync( this.Connection.ApiUrl + this.Connection.TenantId.ToString() + "/" + Entity + "/" + Id.ToString());

            Job job;
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                job = JsonConvert.DeserializeObject<Job>(result.Content);

                if (Wait)
                {

                    JobService jobService = new JobService(this.Connection);

                    Task<CoreLibrary.Models.Job> callTask;

                    do
                    {
                        callTask = Task.Run(() => jobService.GetAsync(job.Id));

                        callTask.Wait();
                        job = callTask.Result;

                        Thread.Sleep(new TimeSpan(0, 0, 5));

                    }
                    while (job.State != "failed" && job.State != "succeeded");

                    return job;

                }
                else
                {
                    return job;
                }

            }
            else
            {
                return null;
            }
        }


    }
}
