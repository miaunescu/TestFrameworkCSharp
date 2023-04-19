using FluentAssertions;
using FluentAssertions.Execution;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestFrame.Base;
using TestFrame.Fixtures;
using TestFrame.Models.TheCatApi;
using Xunit;
using Xunit.Abstractions;

namespace TestFrame.Tests.TheCatAPITests
{
    public class TheCatAPITests : OrderedAcceptanceTestsBase<TheCatApiTestFixture>
    {
        public TheCatAPITests(TheCatApiTestFixture testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
        {
            TestFixture.ApiKey = config.GetSection("TheCatApi")["ApiKey"];
            var api = config.GetSection("TheCatApi")["TheCatApiUri"];
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
        }

        [Fact, TestPriority(2)]
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
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                responsePost.Should().NotBeNull();
                responsePost.ImageId.Should().Be(createCatVotesModel.ImageId);
                responsePost.SubId.Should().Be(createCatVotesModel.SubId);
                responsePost.Value.Should().Be(createCatVotesModel.Value);
            }
        }

        [Fact, TestPriority(3)]
        public async Task Delete_Vote_Test()
        {
            var request = new RestRequest($"/v1/votes/{TestFixture.VoteId}", Method.Delete);
            request.AddHeader("x-api-key", $"{TestFixture.ApiKey}");
            request.AddHeader("exchangeId", TestFixture.CorrelationId);


            var response = await TestFixture.Client.ExecuteAsync<DeleteVoteResponseModel>(request);
            var responseDelete = response.Data;

            using(new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                responseDelete.Message.Should().Be("SUCCESS");
            }
        }

        [Fact]
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
                response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
                responsePost.Should().BeNull();             
            }
        }
    }
}
