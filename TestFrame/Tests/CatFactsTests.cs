using FluentAssertions;
using FluentAssertions.Execution;
using RestSharp;
using System.Net;
using TestFrame.Base;
using TestFrame.Builder;
using TestFrame.Fixtures;
using TestFrame.Models;
using Xunit.Abstractions;

namespace TestFrame.Tests
{
    public class CatFactsTests : AcceptanceTestsBase<CatsTestsFixture>
    {
        private RestBuilder restBuilder = new RestBuilder();
        private RestFactory restFactory;
        // private readonly IRestClientFactoryBuilder _restClientFactoryBuilder;
        public CatFactsTests(CatsTestsFixture testfixture, ITestOutputHelper outputHelper) : base(testfixture, outputHelper)
        {
            var api = config.GetSection("TestData")["CatsApiUri"];
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
            TestFixture.Limit = int.Parse(config.GetSection("TestData")["Limit"]);
            //_restClientFactoryBuilder = restClientFactoryBuilder;
            restFactory = new RestFactory(restBuilder);
        }

        [FactSequence(1)]
        public async Task Get_Cats_Test()
        {


            var response = await restFactory.Create()
                                            .WithRequest("/breeds", Method.Get)
                                            .WithQueryParameter("limit", $"{TestFixture.Limit}")
                                            .Execute<GetCatsModel>(TestFixture.Client);
            var getResponse = response.Data;

            #region Assertions
            using (new AssertionScope())
            {
                getResponse.Data.ElementAt(0).breed.Should().Be("Abyssinian");
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }

            #endregion

        }

    }
}
