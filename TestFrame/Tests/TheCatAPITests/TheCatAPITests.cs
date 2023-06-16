using FluentAssertions;
using FluentAssertions.Execution;
using RestSharp;
using System.Net;
using TestFrame.Base;
using TestFrame.Fixtures;
using TestFrame.Models.TheCatApi;
using Xunit;
using Xunit.Abstractions;
using static TestFrame.Attributes.TestCaseAttribute;

namespace TestFrame.Tests.TheCatAPITests
{
    public class TheCatAPITests : OrderedAcceptanceTestsBase<TheCatApiTestFixture>
    {
        private const string _theCatApi = "TheCatApi";
        public TheCatAPITests(TheCatApiTestFixture testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
        {
            TestFixture.ApiKey = config.GetSection(_theCatApi)["ApiKey"];
            var api = config.GetSection(_theCatApi)["TheCatApiUri"];
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
        }

        [Fact, TestPriority(2)]
        [TestCategory("API", "CatsApi")]

        public async Task Get_Cats_Votes_Test()
        {
            var request = new RestRequest($"/v1/votes/{TestFixture.VoteId}", Method.Get);
            request.AddHeader("x-api-key", $"{TestFixture.ApiKey}");
            request.AddHeader("exchangeId", TestFixture.CorrelationId);

            var response = await TestFixture.Client.ExecuteAsync<GetCatVotesModel>(request);
            var responseGet = response.Data;

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                responseGet.Should().NotBeNull();
                responseGet.CountryCode.Should().Be("RO");
                responseGet.Id.Should().Be(TestFixture.VoteId);
            }
        }

        [Fact, TestPriority(1)]
        [TestCategory("API", "CatsApi")]
        public async Task Create_Cats_Votes_Test()
        {
            var request = new RestRequest("/v1/votes", Method.Post);
            request.AddHeader("x-api-key", $"{TestFixture.ApiKey}");
            request.AddHeader("exchangeId", TestFixture.CorrelationId);

            var createCatVotesModel = new CreateCatVotesModel()
            {
                ImageId = "BkIEhN3pG",
                SubId = "dasda",
                Value = 1
            };

            request.AddBody(createCatVotesModel);

            var response = await TestFixture.Client.ExecuteAsync<CreateCatVotesResponseModel>(request);
            var responsePost = response.Data;

            TestFixture.VoteId = responsePost.Id;

            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                responsePost.Should().NotBeNull();
                responsePost.ImageId.Should().NotBeNullOrEmpty().And.Be(createCatVotesModel.ImageId);
                responsePost.SubId.Should().NotBeNullOrEmpty().And.Be(createCatVotesModel.SubId);
                responsePost.Value.Should().BeGreaterThan(0).And.Be(createCatVotesModel.Value);
            }
        }

        [Fact, TestPriority(3)]
        [TestCategory("API", "CatsApi")]
        public async Task Delete_Vote_Test()
        {
            var request = new RestRequest($"/v1/votes/{TestFixture.VoteId}", Method.Delete);
            request.AddHeader("x-api-key", $"{TestFixture.ApiKey}");
            request.AddHeader("exchangeId", TestFixture.CorrelationId);


            var response = await TestFixture.Client.ExecuteAsync<DeleteVoteResponseModel>(request);
            var responseDelete = response.Data;

            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                responseDelete.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                responseDelete.Message.Should().Be("SUCCESS");
            }
        }

        [Fact]
        [TestCategory("API", "CatsApi")]

        public async Task Create_Cats_Vote_Negative_Test()
        {
            var request = new RestRequest("/v1/votes", Method.Post);
            request.AddHeader("exchangeId", TestFixture.CorrelationId);


            var createCatVotesModel = new CreateCatVotesModel()
            {
                ImageId = "BkIEhN3pG",
                SubId = "dasda",
                Value = 1
            };

            request.AddBody(createCatVotesModel);

            var response = await TestFixture.Client.ExecuteAsync<CreateCatVotesResponseModel>(request);
            var responsePost = response.Data;

            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
                responsePost.Should().BeNull();
            }
        }
    }
}
