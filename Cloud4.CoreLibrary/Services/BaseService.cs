// Copyright (c) HIAG Data AG. All Rights Reserved. Licensed under the GNU License.  See License.txt
using Cloud4.CoreLibrary.Client;
using Cloud4.CoreLibrary.Models;
using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Services
{
    public class BaseService<T,Y,Z> : IBaseServiceInterface<T>, IBaseCreateServiceInterface<Y>, IBaseUpdateServiceInterface<Z>
    {
        public Connection Connection { get; set; }

        protected ResourceDataClient client { get; set; }

        public delegate void OnRefreshConnection();
        public event OnRefreshConnection OnRefreshConnectionRaised;

        protected string Entity { get; set; }

        public BaseService()
        {

        }
        public BaseService(Connection con)
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

        public virtual async Task<Result<List<T>>> AllAsync()
        {
            Result<List<T>> returnresult = new Result<List<T>>();
            var result = await client.GetDataAsJsonAsync<List<T>>( new Uri(this.Connection.ApiUrl, Entity));

            returnresult.Object = result.Content;
            returnresult.Code = result.StatusCode;

            return returnresult;


        }

        public virtual async Task<Result<T>> GetAsync(Guid Id)
        {
            Result<T> returnresult = new Result<T>();
            var result = await client.GetDataAsJsonAsync<T>(new Uri(this.Connection.ApiUrl, Entity + "/" + Id.ToString()));

            returnresult.Object = result.Content;
            returnresult.Code = result.StatusCode;

            return returnresult;


        }

        public virtual async Task<Result> CreateAsync(Y body)
        {
            Result returnresult = new Result();
            DataClientResult result = await client.PostDataAsJsonAsync<Y>( this.Connection.ApiUrl + Entity, body);

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
            DataClientResult result = await client.PutDataAsJsonAsync<Z>(new Uri(this.Connection.ApiUrl, Entity + "/" + Id.ToString()), body);

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

        public virtual async Task<Result> DeleteAsync(Guid Id, bool Wait)
        {
            Result returnresult = new Result();
            DataClientResult result = await client.DeleteDataAsJsonAsync(new Uri(this.Connection.ApiUrl, Entity + "/" + Id.ToString()));


            Job job;
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                job = JsonConvert.DeserializeObject<Job>(result.Content);



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
