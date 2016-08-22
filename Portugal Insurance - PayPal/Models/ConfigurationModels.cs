using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portugal_Insurance___PayPal.Models
{
    public static class Configuration
    {

        //these variables will store the clientID and clientSecret
        //by reading them from web.config
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        static Configuration()
        {
            var paypalConfig = GetConfig();
            ClientId = paypalConfig["clientId"];
            ClientSecret = paypalConfig["clientSecret"];
        }

        //getting properties from the web.config
        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        { 
            //getting accesstoken from PayPal
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();

            return accessToken;
        }
            public static APIContext GetAPIContext()
            {
                //return apicontext object by invocking it with the accesstoken
                APIContext apiContext = new APIContext(GetAccessToken());
                apiContext.Config = GetConfig();
                return apiContext;
            }
        }
    }