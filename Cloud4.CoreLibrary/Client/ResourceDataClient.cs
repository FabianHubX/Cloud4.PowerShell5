using Cloud4.CoreLibrary.Models;
using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Client
{
    public class ResourceDataClient
    {

        public Connection Connection { get; set; }


        public delegate void OnRefreshToken();
        public event OnRefreshToken OnRefreshTokenRaised;

   


        public async Task<DataClientResult> PostDataAsJsonAsync<T>( string URL, T data)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.SetBearerToken(this.Connection.AccessToken);

            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            System.Uri uri = new System.Uri(URL);

            var payloadFromResourceServer = await httpClient.PostAsync(uri, content).ConfigureAwait(false);
             return !payloadFromResourceServer.IsSuccessStatusCode ? 
                new DataClientResult {
                    StatusCode = payloadFromResourceServer.StatusCode, Content = ""
                } : new DataClientResult {
                    StatusCode = payloadFromResourceServer.StatusCode, Content = await payloadFromResourceServer.Content.ReadAsStringAsync().ConfigureAwait(false)
                };


        }

        public async Task<DataClientResult> PutDataAsJsonAsync<T>( Uri uri, T data)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.SetBearerToken(this.Connection.AccessToken);

            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
          

            var payloadFromResourceServer = await httpClient.PutAsync(uri, content).ConfigureAwait(false);
            return !payloadFromResourceServer.IsSuccessStatusCode ?
                new DataClientResult
                {
                    StatusCode = payloadFromResourceServer.StatusCode,
                    Content = ""
                } : new DataClientResult
                {
                    StatusCode = payloadFromResourceServer.StatusCode,
                    Content = await payloadFromResourceServer.Content.ReadAsStringAsync().ConfigureAwait(false)
                };


        }


        public async Task<DataClientResult> DeleteDataAsJsonAsync(Uri URL)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.SetBearerToken(this.Connection.AccessToken);
           

            var payloadFromResourceServer = await httpClient.DeleteAsync(URL);
            if (!payloadFromResourceServer.IsSuccessStatusCode)
            {
               
                return new DataClientResult { StatusCode = payloadFromResourceServer.StatusCode, Content = "" };
            }
            else
            {
                

                return new DataClientResult { StatusCode = payloadFromResourceServer.StatusCode, Content = await payloadFromResourceServer.Content.ReadAsStringAsync() };

            }
        }


        public async Task<DataClientResult<T>> GetDataAsJsonAsync<T>( Uri URL)
        {
         
            HttpClient httpClient = new HttpClient();
            httpClient.SetBearerToken(this.Connection.AccessToken);
            System.IO.Stream content;

            var payloadFromResourceServer = await httpClient.GetAsync(URL);
            if (payloadFromResourceServer.IsSuccessStatusCode)
            {

                content = await payloadFromResourceServer.Content.ReadAsStreamAsync();
                string result;

                using (StreamReader reader = new StreamReader(content, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }

                try
                {

                    var objects = JsonConvert.DeserializeObject<T>(result);
                    return new DataClientResult<T> { StatusCode = payloadFromResourceServer.StatusCode, Content = objects };
                }
                catch (Exception e)
                {
                    throw new Exception("Deserialize Object Failed");
                }

                return new DataClientResult<T> { StatusCode = payloadFromResourceServer.StatusCode, Content = default(T) };

            }
            else if (payloadFromResourceServer.StatusCode == HttpStatusCode.Unauthorized & !string.IsNullOrEmpty(Connection.AccessToken))
            {
                RefreshCredentials();

                httpClient.SetBearerToken(Connection.AccessToken);

                var payloadretryFromResourceServer = await httpClient.GetAsync(URL);
                if (payloadretryFromResourceServer.IsSuccessStatusCode)
                {

                    content = await payloadretryFromResourceServer.Content.ReadAsStreamAsync();
                    string result;

                    using (StreamReader reader = new StreamReader(content, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }

                    var objects = JsonConvert.DeserializeObject<T>(result);

                    return new DataClientResult<T> { StatusCode = payloadFromResourceServer.StatusCode, Content = objects } ;

                }
                else
                {
                    return new DataClientResult<T> {  StatusCode = payloadFromResourceServer.StatusCode, Content = default(T) };
                }
            }
            else
            {
                return new DataClientResult<T> { StatusCode = payloadFromResourceServer.StatusCode, Content = default(T) };
            }


        }


        private void RefreshCredentials()
        {
            IdentityServer4Client.StsServer = this.Connection.LogonUrl;
            var Response = IdentityServer4Client.LoginAsync(this.Connection.UserName, this.Connection.PassWord).Result;           

            this.Connection.AccessToken= Response.AccessToken;

            if (OnRefreshTokenRaised != null)
            {
                OnRefreshTokenRaised();
            }
        }
    }


    public class DataClientResult
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Content { get; set; }
    }

    public class DataClientResult<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public T Content { get; set; }
    }
}
