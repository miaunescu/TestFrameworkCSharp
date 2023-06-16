using RestSharp;

namespace TestFrame.Builder
{
    public interface IRestBuilder
    {
        IRestBuilder WithRequest(string request, RestSharp.Method method);
        IRestBuilder WithHeader(string name, string value);
        IRestBuilder WithQueryParameter(string name, string value);
        IRestBuilder WithBody(object body);
        Task<RestResponse<T>> Execute<T>(RestClient client);
        IRestBuilder Create();
    }
}
