using RestSharp;

namespace TestFrame.Builder
{
    public class RestBuilder : IRestBuilder
    {
        private RestRequest Request;
        public IRestBuilder Create() => this;
        public IRestBuilder WithRequest(string request, RestSharp.Method method)
        {
            Request = new RestRequest(request);
            return this;
        }

        public IRestBuilder WithHeader(string name, string value)
        {
            Request.AddHeader(name, value);
            return this;
        }

        public IRestBuilder WithQueryParameter(string name, string value)
        {
            Request.AddQueryParameter(name, value);
            return this;
        }

        public IRestBuilder WithBody(object body)
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
