// Copyright (c) HIAG Data AG. All Rights Reserved. Licensed under the GNU License.  See License.txt
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
    public class BaseLoadBalancerService<T,Y,Z> : IBaseServiceInterface<T>, IBaseCreateServiceInterface<Y>, IBaseUpdateServiceInterface<Z>
    {
        protected Connection Connection { get; set; }

        protected ResourceDataClient client { get; set; }

        public delegate void OnRefreshConnection();
        public event OnRefreshConnection OnRefreshConnectionRaised;

        protected string Entity { get; set; }

        protected string SubEntity { get; set; }

        protected Guid LoadBalancerId { get; set; }

        public BaseLoadBalancerService()
        {

        }
        public BaseLoadBalancerService(Connection con, Guid LoadBalancerId)
        {
            client = new ResourceDataClient();
            client.Connection = con;
            this.Connection = con;
            this.LoadBalancerId = LoadBalancerId;

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

        public virtual async Task<Result<List<T>>> AllAsync()
        {
            Result<List<T>> returnresult = new Result<List<T>>();

            var result = await client.GetDataAsJsonAsync<List<T>>( new Uri( this.Connection.ApiUrl , this.Connection.TenantId.ToString() + "/" + Entity + "/" + LoadBalancerId.ToString() + "/" + SubEntity));

            returnresult.Object = result.Content;
            returnresult.Code = result.StatusCode;

            return returnresult;


        }

        public virtual async Task<Result<T>> GetAsync(Guid Id)
        {
            Result<T> returnresult = new Result<T>();

            var result = await client.GetDataAsJsonAsync<T>( new Uri( this.Connection.ApiUrl , this.Connection.TenantId.ToString() + "/" + Entity + "/" + LoadBalancerId.ToString() + "/" + SubEntity + "/" + Id.ToString()));

            returnresult.Object = result.Content;
            returnresult.Code = result.StatusCode;

            return returnresult;

        }

        public virtual async Task<Result> CreateAsync(Y body)
        {
            Result returnresult = new Result();
            DataClientResult result = await client.PostDataAsJsonAsync<Y>( this.Connection.ApiUrl + this.Connection.TenantId.ToString() + "/" + Entity + "/" + LoadBalancerId.ToString() + "/" + SubEntity, body);

            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var job = JsonConvert.DeserializeObject<Job>(result.Content);
                returnresult.Job = job;
                return returnresult;
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                returnresult.Code = result.StatusCode;
                return returnresult;
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                returnresult.Error = JsonConvert.DeserializeObject<ErrorDetails>(result.Content);
                return returnresult;
            }

            returnresult.Code = result.StatusCode;
            return returnresult;
        }

        public virtual async Task<Result> UpdateAsync(Guid Id, Z body)
        {
            Result returnresult = new Result();
            DataClientResult result = await client.PutDataAsJsonAsync<Z>(new Uri(this.Connection.ApiUrl, this.Connection.TenantId.ToString() + "/" + Entity + "/" + LoadBalancerId.ToString() + "/" + SubEntity + "/" + Id.ToString()), body);

            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var job = JsonConvert.DeserializeObject<Job>(result.Content);
                returnresult.Job = job;
                return returnresult;
            }          
            else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                returnresult.Code = result.StatusCode;
                return returnresult;
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                returnresult.Error = JsonConvert.DeserializeObject<ErrorDetails>(result.Content);
                return returnresult;
            }

            returnresult.Code = result.StatusCode;
            return returnresult; return returnresult;
            
        }

        public virtual async Task<Result> DeleteAsync(Guid Id, bool Wait)
        {
            Result returnresult = new Result();
            DataClientResult result = await client.DeleteDataAsJsonAsync(new Uri(this.Connection.ApiUrl, this.Connection.TenantId.ToString() + "/" + Entity + "/" + LoadBalancerId.ToString() + "/" + SubEntity + "/" + Id.ToString()));

            Job job;
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                job = JsonConvert.DeserializeObject<Job>(result.Content);

                if (Wait)
                {

                    JobService jobService = new JobService(this.Connection);

                    Task<CoreLibrary.Models.Result<Job>> callTask;

                    do
                    {
                        callTask = Task.Run(() => jobService.GetAsync(job.Id));

                        callTask.Wait();
                        job = callTask.Result.Object;

                        Thread.Sleep(new TimeSpan(0, 0, 5));

                    }
                    while (job.State != "failed" && job.State != "succeeded");

                    returnresult.Job = job;
                    return returnresult;

                }
                else
                {
                    returnresult.Job = job;
                    return returnresult;
                }

            }
            else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                returnresult.Code = result.StatusCode;
                return returnresult;
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                returnresult.Error = JsonConvert.DeserializeObject<ErrorDetails>(result.Content);
                return returnresult;
            }

            returnresult.Code = result.StatusCode;
            return returnresult;
        }


    }
}
