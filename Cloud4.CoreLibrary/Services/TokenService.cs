// Copyright (c) HIAG Data AG. All Rights Reserved. Licensed under the GNU License.  See License.txt
using Cloud4.CoreLibrary.Client;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud4.CoreLibrary.Services
{
    public static class TokenService
    {
        public static string AccessToken { get; set; }

        public static string RefreshToken { get; set; }

        public static DateTime ExpiresAt { get; set; }

        private static TokenResponse Response { get; set; }



        public static void Connect(Uri logonUrl, string username, string password)
        {
 

            IdentityServer4Client.StsServer = logonUrl;
            Response = IdentityServer4Client.LoginAsync(username,password).Result;

            RefreshToken = Response.RefreshToken;

            ExpiresAt = DateTime.UtcNow.AddSeconds(Response.ExpiresIn);

            AccessToken = Response.AccessToken;

        

        }

        public static void Refresh(Uri logonUrl, string refreshToken)
        {


            IdentityServer4Client.StsServer = logonUrl;
            Response = IdentityServer4Client.RefreshTokenAsync(refreshToken).Result;

            RefreshToken = Response.RefreshToken;

            ExpiresAt = DateTime.UtcNow.AddSeconds(Response.ExpiresIn);

            AccessToken = Response.AccessToken;



        }
    }
}
