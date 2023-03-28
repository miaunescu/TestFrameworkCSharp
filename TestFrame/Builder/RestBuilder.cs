using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestFrame.Builder
{
    public class RestBuilder
    {
        private RestRequest Request;
            
        public RestBuilder WithRequest(string request, RestSharp.Method method)
        {
            Request = new RestRequest(request);
            return this;
        }

        public RestBuilder WithHeader(string name, string value)
        {
            Request.AddHeader(name, value);
            return this;
        }

        public RestBuilder WithQueryParameter(string name, string value)
        {
            Request.AddQueryParameter(name, value);
            return this;
        }

        public RestBuilder WithBody(object body)
        {
            Request.AddJsonBody(body);
            return this;
        }

        public async Task<RestResponse<T>> Execute<T>(RestClient client)
        {
            var response = await client.ExecuteAsync<T>(Request);
            return response;
        }
    }
}
