using RestSharp;

namespace TestFrame
{
    public static class RestClientFactory
    {
        public static RestClient CreateBasicClient(string baseApi)
        {
            RestClient client = new RestClient(baseApi)
            {

            };

            return client;
        }

    }
}
