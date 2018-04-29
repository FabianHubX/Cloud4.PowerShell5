using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

using System;
using System.Text;
using System.Threading.Tasks;

namespace Cloud4.CoreLibrary.Client
{
    public static class IdentityServer4Client
    {
        private static TokenClient _tokenClient;

        private static Uri stsServer;

        public static Uri StsServer { get => stsServer; set => stsServer = value; }

        public static async Task<TokenResponse> LoginAsync(string user, string password)
        {
            Console.Title = "Console ResourceOwner Flow RefreshToken";

            // https://sts.e2e.itnetx.ch/.well-known/openid-configuration
            // https://github.com/IdentityModel/IdentityModel.OidcClient2/issues/46
            // var disco = await DiscoveryClient.GetAsync(StsServer);
            // if (disco.IsError) throw new Exception(disco.Error);

            var client = new DiscoveryClient(stsServer.ToString());
            client.Policy.ValidateIssuerName = false; // "https://sts.fes.d1.sorbus.cloud:44354"

            var disco = await client.GetAsync();
            if (disco.IsError) throw new Exception(disco.Error);

            _tokenClient = new TokenClient(
                disco.TokenEndpoint,
                "script_client",
                "script_api");

            return await RequestTokenAsync(user, password);
        }

        public static async Task<TokenResponse> RunRefreshAsync(TokenResponse response, int milliseconds)
        {
            var refresh_token = response.RefreshToken;

            while (true)
            {
                response = await RefreshTokenAsync(refresh_token);

                // Get the resource data using the new tokens...
              //  await ResourceDataClient.GetDataAndDisplayInConsoleAsync(response.AccessToken);

                if (response.RefreshToken != refresh_token)
                {
                    ShowResponse(response);
                    refresh_token = response.RefreshToken;
                }

                Task.Delay(milliseconds).Wait();
            }

            return response;
        }
        private static async Task<TokenResponse> RequestTokenAsync(string user, string password)
        {
            //Log.Logger.Verbose("begin RequestTokenAsync");
            return await _tokenClient.RequestResourceOwnerPasswordAsync(
                user,
                password,
                "email openid public_api offline_access");
        }

        public static async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            //Log.Logger.Verbose("Using refresh token: {RefreshToken}", refreshToken);

            return await _tokenClient.RequestRefreshTokenAsync(refreshToken);
        }

        private static void ShowResponse(TokenResponse response)
        {
            if (!response.IsError)
            {
                //Log.Logger.Debug("Token response: {TokenPayload}", response.Json.ToString());

                if (response.AccessToken.Contains("."))
                {
                    var parts = response.AccessToken.Split('.');
                    var header = parts[0];
                    var claims = parts[1];

                    //Log.Logger.Debug("Access Token Header decoded {AccessHeader}", JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(header))).ToString());
                    //Log.Logger.Debug("Access Token claims decoded {Claims}", JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(claims))).ToString());
                }
            }
            else
            {
                if (response.ErrorType == ResponseErrorType.Http)
                {
                    //Log.Logger.Warning("HTTP error:  {ResponseError}", response.Error);
                    //Log.Logger.Warning("HTTP status code:  {ResponseHttpStatusCode}", response.HttpStatusCode);
                }
                else
                {
                    //Log.Logger.Warning("Protocol error response: {ResponsePayload}", response.Json);
                }
            }
        }
    }
}
