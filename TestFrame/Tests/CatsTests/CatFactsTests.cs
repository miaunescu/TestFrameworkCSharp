using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualBasic.FileIO;
using RestSharp;
using System.Net;
using TestFrame.Base;
using TestFrame.Builder;
using TestFrame.Fixtures;
using TestFrame.Models.CatsModels;
using Xunit;
using Xunit.Abstractions;

namespace TestFrame.Tests.CatsTests
{
    public class CatFactsTests : OrderedAcceptanceTestsBase<CatsTestsFixture>
    {
        private RestBuilder restBuilder = new RestBuilder();
        private RestFactory restFactory;

        public CatFactsTests(CatsTestsFixture testfixture, ITestOutputHelper outputHelper) : base(testfixture, outputHelper)
        {
            var api = config.GetSection("CatsTestData")["CatsApiUri"];
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
            TestFixture.Limit = int.Parse(config.GetSection("CatsTestData")["Limit"]);
            TestFixture.MaxLength = int.Parse(config.GetSection("CatsTestData")["MaxLength"]);
            restFactory = new RestFactory(restBuilder);
        }

        [Fact, TestPriority(1)]
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
                getResponse.Data.Should().NotBeNullOrEmpty();
                getResponse.Data.ElementAt(0).Should().NotBeNull();

                getResponse.Data.ElementAt(0).Breed.Should().Be("Abyssinian");
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            #endregion
        }

        [Fact, TestPriority(2)]
        public async Task GetBreeds_NegativeResponse()
        {
            var response = await restFactory.Create()
                                            .WithRequest("/breeds", Method.Get)
                                            .WithQueryParameter("limit", $"{-1}")
                                            .Execute<GetCatsModel>(TestFixture.Client);
            var getResponse = response.Data;

            #region Assertions
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
            #endregion
        }

        [Fact, TestPriority(3)]
        public async Task CheckGetBreedsResponse()
        {
            var response = await restFactory.Create()
                                            .WithRequest("/breeds", Method.Get)
                                            .WithQueryParameter("limit", $"{TestFixture.Limit}")
                                            .Execute<GetCatsModel>(TestFixture.Client);
            var getResponse = response.Data;

            #region Assertions
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                
                getResponse.Data.Should().NotBeNullOrEmpty();
                getResponse.Links.Should().NotBeNullOrEmpty();
                getResponse.PerPage.Should().NotBeNullOrEmpty();

                getResponse.Data.Count().Should().BeLessThanOrEqualTo(TestFixture.Limit);
                getResponse.Links[1].Active.Should().Be(true);
                getResponse.PerPage.Should().Be($"{TestFixture.Limit}");
            }
            #endregion
        }

        [Fact, TestPriority(4)]
        public async Task GetCatFact_CheckData()
        {
            var response = await restFactory.Create()
                                            .WithRequest("/fact", Method.Get)
                                            .WithQueryParameter("max_length", $"{TestFixture.MaxLength}")
                                            .Execute<CatDetailsModel>(TestFixture.Client);
            var getResponse = response.Data;

            #region Assertions
            using (new AssertionScope())
            {
                getResponse.Fact.Should().NotBeNullOrEmpty();
                getResponse.Fact.Should().Be("Cats have 3 eyelids.");
                getResponse.Length.Should().Be(20);
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            #endregion
        }

        [Fact, TestPriority(5)]
        public async Task GetCatFact_CheckResponseBodyEmpty()
        {
            var response = await restFactory.Create()
                                            .WithRequest("/fact", Method.Get)
                                            .WithQueryParameter("max_length", $"{-1}")
                                            .Execute<CatDetailsModel>(TestFixture.Client);
            var getResponse = response.Data;

            #region Assertions
            using (new AssertionScope())
            {
                getResponse.Breed.Should().BeNull();
                getResponse.Coat.Should().BeNull();
                getResponse.Country.Should().BeNull();
                getResponse.Fact.Should().BeNull();
                getResponse.Origin.Should().BeNull();
                getResponse.Pattern.Should().BeNull();
                getResponse.Length.Should().Be(0);

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            #endregion
        }

        [Fact, TestPriority(6)]
        public async Task Get_CatFacts_CheckData()
        {
            var response = await restFactory.Create()
                                            .WithRequest("/facts", Method.Get)
                                            .WithQueryParameter("max_length",$"{30}")
                                            .WithQueryParameter("limit", $"{TestFixture.Limit}")
                                            .Execute<GetCatsModel>(TestFixture.Client);
            var getResponse = response.Data;

            #region Assertions
            using (new AssertionScope())
            {
                getResponse.Data.Should().NotBeNullOrEmpty();

                getResponse.PerPage.Should().Be($"{TestFixture.Limit}");
                getResponse.Data[3].Fact.Should().Be("Cats dislike citrus scent.");
                getResponse.Data[3].Length.Should().Be(26);
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            #endregion
        }

        [Fact, TestPriority(7)]
        public async Task CatFacts_TestAllFactLengthsSmallerOrEqualTo30()
        {
            var response = await restFactory.Create()
                                            .WithRequest("/facts", Method.Get)
                                            .WithQueryParameter("max_length", $"{30}")
                                            .WithQueryParameter("limit",$"{30}")
                                            .Execute<GetCatsModel>(TestFixture.Client);
            var getResponse = response.Data;

            #region Assertions
            using (new AssertionScope())
            {
                getResponse.Data.Should().NotBeNullOrEmpty();
                getResponse.Data.All(x => x.Length <= 30).Should().BeTrue();
                getResponse.PerPage.Should().Be($"{30}");
            }
            #endregion
        }

        [Fact, TestPriority(8)]
        public async Task CatFacts_CheckDifferentFields()
        {
            var response = await restFactory.Create()
                                            .WithRequest("/facts", Method.Get)
                                            .WithQueryParameter("max_length", $"{300}")
                                            .WithQueryParameter("limit", $"{10}")
                                            .Execute<GetCatsModel>(TestFixture.Client);
            var getResponse = response.Data;


            #region Assertions
            using (new AssertionScope())
            {
                getResponse.Should().NotBeNull();
                getResponse.Data.Should().NotBeNullOrEmpty();
                getResponse.Links.Should().NotBeNullOrEmpty();
                getResponse.PerPage.Should().NotBeNullOrEmpty();

                getResponse.PerPage.Should().Be($"{10}");
                getResponse.From.Should().Be(1);
                getResponse.LastPage.Should().Be(33);
                getResponse.Links[1].Active.Should().BeTrue();
                getResponse.Links[14].Label.Should().Be("Next");
                getResponse.Links[14].Url.Should().Be("https://catfact.ninja/facts?page=2");
                getResponse.Total.Should().Be(325);
            }
            #endregion
        }
    }
}
