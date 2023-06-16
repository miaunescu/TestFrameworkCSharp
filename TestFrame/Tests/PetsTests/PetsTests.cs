using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using TestFrame.Base;
using TestFrame.Fixtures;
using TestFrame.Models.PetsModels;
using Xunit;
using Xunit.Abstractions;

namespace TestFrame.Tests.PetsTests
{
    public class PetsTests : OrderedAcceptanceTestsBase<PetsTestFixture>

    {
        private const string _petsTestData = "PetsTestData";
        public PetsTests(PetsTestFixture testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
        {
            var api = config.GetSection(_petsTestData)["PetstoreApiUri"];
            TestFixture.PetID = int.Parse(config.GetSection(_petsTestData)["PetID"]!);
            TestFixture.InvalidID = int.Parse(config.GetSection(_petsTestData)["InvalidID"]!);
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
        }

        //Get 
        [Fact, TestPriority(1)]
        public async Task Get_Pet_By_Id_Test()
        {
            var request = new RestRequest($"/v2/pet/{TestFixture.PetID}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseContent = JsonConvert.DeserializeObject(response.Content) as JObject;
            var categoryProperty = responseContent?.GetValue("category");
            var categoryResponse = categoryProperty?.ToObject<CategoryModel>();

            var responseGetPet = response.Content;

            #region Asserts
            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Content.Should().NotBeNull();

                categoryResponse!.Id.Should().Be(TestFixture.PetID);
                responseGetPet.Should().NotBeNull();
            }
            #endregion
        }

        [Fact, TestPriority(1)]
        public async Task Get_Pet_By_InvalidId_Test()
        {
            var request = new RestRequest($"/v2/pet/{TestFixture.InvalidID}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseError = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);

            #region Asserts
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
                responseError.Message.Should().Be("Pet not found");
            }
            #endregion
        }

        [Theory, TestPriority(1)]
        [InlineData("available")]
        [InlineData("pending")]
        [InlineData("sold")]
        public async Task Get_Pet_By_Status_Test(string status)
        {
            var request = new RestRequest($"/v2/pet/findByStatus?status={status}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseGetPet = response.Content;

            #region Asserts
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                response.Content!.Should().NotBeNullOrEmpty();
                responseGetPet.Should().NotBeNull();
            }
            #endregion
        }


        [Fact, TestPriority(2)]
        public async Task Add_New_Pet_Test()
        {
            var request = new RestRequest($"/v2/pet", Method.Post);

            var monkey = new GetPetModel()
            {
                Category = new CategoryModel()
                {
                    Name = "Sisi"
                },

                Name = "Monkey",
                Status = "Available"
            };

            request.AddJsonBody(monkey);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseContent = JsonConvert.DeserializeObject(response.Content) as JObject;
            var categoryProperty = responseContent!.GetValue("category");
            var categoryResponse = categoryProperty.ToObject<CategoryModel>();

            var responseGetPet = response.Content;

            var responsePet = JsonConvert.DeserializeObject<GetPetModel>(response.Content);
            this.TestFixture.Id = responsePet.Id;

            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                responsePet.Should().NotBeNull();
                responseGetPet.Should().NotBeNull();
                responsePet.Name.Should().NotBeNull();
                responsePet.Id.Should().BeGreaterThan(0);

                responsePet.Name.Should().Be(monkey.Name);
                responsePet.Status.Should().Be(monkey.Status);
            }
        }

        [Fact, TestPriority(3)]
        public async Task Update_New_Pet_Test()
        {
            var request = new RestRequest($"/v2/pet", Method.Put);

            var monkey = new GetPetModel()
            {
                Category = new CategoryModel()
                {
                    Name = "Sisi - updated"
                },

                Name = "Monkey",
                Status = "pending"
            };

            request.AddJsonBody(monkey);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseContent = JsonConvert.DeserializeObject(response.Content) as JObject;
            var categoryProperty = responseContent!.GetValue("category");

            var responseGetPet = response.Content;

            var responsePet = JsonConvert.DeserializeObject<GetPetModel>(response.Content);

            TestFixture.Id = responsePet.Id;

            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                responsePet.Should().NotBeNull();
                responsePet.Name.Should().NotBeNull();
                responsePet.Status.Should().NotBeNull();
                responsePet.Id.Should().BeGreaterThan(0);
                responsePet.Status.Should().Be(monkey.Status);
            }
        }

        [Fact, TestPriority(4)]
        public async Task Update_New_Pet_By_PetId_Test()
        {
            var request = new RestRequest($"/v2/pet/{TestFixture.Id}", Method.Post);
            request.AddQueryParameter("petId", TestFixture.Id);
            request.AddQueryParameter("name", "Sisi");
            request.AddQueryParameter("status", "sold");

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseGetPet = response.Content;

            #region Asserts
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();
                responseGetPet.Should().NotBeNull();
            }
            #endregion
        }

        [Fact, TestPriority(5)]
        public async Task Delete_Pet_By_Id_Test()
        {
            var request = new RestRequest($"/v2/pet/{TestFixture.Id}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);

            #region Asserts
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            #endregion
        }

        [Fact, TestPriority(6)]
        public async Task Delete_Pet_By_InvalidId_Test()
        {
            var request = new RestRequest($"/v2/pet/{TestFixture.InvalidID}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);

            #region Asserts
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
            #endregion
        }
    }
}
