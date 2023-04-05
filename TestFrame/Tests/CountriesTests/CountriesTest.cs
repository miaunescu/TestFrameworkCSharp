using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Hosting;
using RestSharp;
using System.Net;
using System.Security.Authentication;
using TestFrame.Base;
using TestFrame.Builder;
using TestFrame.Fixtures;
using TestFrame.Models.CatsModels;
using TestFrame.Models.CountriesModels;
using Xunit;
using Xunit.Abstractions;

namespace TestFrame.Tests
{
    public class CountriesTest : OrderedAcceptanceTestsBase<CountriesTestFixtures>
    {
        private RestBuilder restBuilder = new RestBuilder();
        private RestFactory restFactory;

        public CountriesTest(CountriesTestFixtures testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
        {

            var api = config.GetSection("CountriesTestData")["CountriesApiUri"];
            TestFixture.Name = config.GetSection("CountriesTestData")["Name"];
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
            restFactory = new RestFactory(restBuilder);
        }

        [Fact, TestPriority(1)]
        public async Task Get_Country_By_Name_Test()
        {
            var response = await restFactory.Create()
                                            .WithRequest("/name/Romania", Method.Get)
                                            //.WithQueryParameter("name", $"{TestFixture.Name}")
                                            .Execute<List<CountryModel>>(TestFixture.Client);
            var getResponse = response.Data;
        }

    }
}
