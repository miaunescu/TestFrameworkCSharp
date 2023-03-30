using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestFrame.Base;
using TestFrame.Builder;
using TestFrame.Fixtures;
using TestFrame.Models;
using TestFrame.Models.PetsModels;
using Xunit;
using Xunit.Abstractions;

namespace TestFrame.Tests
{
    public class PetsTests : OrderedAcceptanceTestsBase<PetsTestFixture>
    {
        public PetsTests(PetsTestFixture testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
        {
            var api = config.GetSection("PetsTestData")["PetstoreApiUri"];
            TestFixture.PetID = int.Parse(config.GetSection("PetsTestData")["PetID"]);
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
        }

        [Fact, TestPriority(1)]
        public async Task Get_Pet_By_Id_Test()
        {
            var request = new RestRequest($"/v2/pet/{TestFixture.PetID}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var reponseContent = JsonConvert.DeserializeObject(response.Content) as JObject;
            var categoryProperty = reponseContent.GetValue("category");
            var categoryResponse = categoryProperty.ToObject<CategoryModel>();

            var responseGetPet = response.Content;

            #region Asserts
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Content.FirstOrDefault();
                categoryResponse.Id.Should().Be(TestFixture.PetID);
                responseGetPet.Should().NotBeNull();
            }
            #endregion
        }

    }
}
