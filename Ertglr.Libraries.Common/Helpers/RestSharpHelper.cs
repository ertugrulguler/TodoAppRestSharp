using Ertglr.Libraries.Common.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Ertglr.Libraries.Common.Helpers
{
    public class RestSharpHelper
    {
        private RestClient client = null;
        public RestSharpHelper(string appsettingsKeyToEndPoint)
        {
            if (ConfigurationManager.AppSettings[appsettingsKeyToEndPoint] != null)
                client = new RestClient(ConfigurationManager.AppSettings[appsettingsKeyToEndPoint]);
            else
                throw new Exception($"{appsettingsKeyToEndPoint} değeri bulunamadı.");
        }

        public IRestResponse<T> Get<T>(string url) where T : class, new()
        {
            var request = new RestRequest(url, Method.GET);
            IRestResponse<T> response = client.Execute<T>(request);

            return response;
        }

        public IRestResponse<T> Post<T>(string url, object item) where T : class, new()
        {
            var request = new RestRequest(url, Method.POST);

            request.AddJsonBody(item);
            IRestResponse<T> response = client.Execute<T>(request);

            return response;
        }


        public IRestResponse<T> Put<T>(string url, object item) where T : class, new()
        {
            var request = new RestRequest(url, Method.PUT);

            request.AddJsonBody(item);
            IRestResponse<T> response = client.Execute<T>(request);

            return response;
        }

        public IRestResponse<T> Delete<T>(string url) where T : class, new()
        {
            var request = new RestRequest(url, Method.DELETE);

            IRestResponse<T> response = client.Execute<T>(request);

            return response;
        }
    }
}