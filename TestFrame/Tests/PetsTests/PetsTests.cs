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

namespace TestFrame.Tests.PetsTests
{
    public class PetsTests : OrderedAcceptanceTestsBase<PetsTestFixture>

    {
        

        public PetsTests(PetsTestFixture testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
        {
            var api = config.GetSection("PetsTestData")["PetstoreApiUri"];
            TestFixture.PetID = int.Parse(config.GetSection("PetsTestData")["PetID"]);
            TestFixture.InvalidID = int.Parse(config.GetSection("PetsTestData")["InvalidID"]);
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
        }

        //Get 

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

        [Fact, TestPriority(1)]
        public async Task Get_Pet_By_InvalidId_Test()
        {
            var request = new RestRequest($"/v2/pet/{TestFixture.InvalidID}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            ErrorResponse responseError = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            Assert.True(responseError.Message == "Pet not found");

            #region Asserts
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
                
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
                response.Content.FirstOrDefault();
                responseGetPet.Should().NotBeNull();
            }
            #endregion
        }


        [Fact, TestPriority(2)]
        public async Task Add_New_Pet_Test()
        {
            var request = new RestRequest($"/v2/pet", Method.Post);

            GetPetModel monkey = new GetPetModel();

            monkey.Category = new CategoryModel();
            monkey.Category.Name = "Sisi";
            monkey.Name = "Monkey";
            monkey.Status = "Available";

            request.AddJsonBody(monkey);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var reponseContent = JsonConvert.DeserializeObject(response.Content) as JObject;
            var categoryProperty = reponseContent.GetValue("category");
            var categoryResponse = categoryProperty.ToObject<CategoryModel>();
            
            var responseGetPet = response.Content;

            GetPetModel responsePet = JsonConvert.DeserializeObject<GetPetModel>(response.Content);

            Assert.NotNull(responsePet);
            Assert.NotEqual(0,responsePet.Id);
            Assert.NotNull(responsePet.Name);
            Assert.Equal(monkey.Name, responsePet.Name);
            Assert.NotNull(responsePet.Status);
            Assert.Equal(monkey.Status, responsePet.Status);

            this.TestFixture.Id = responsePet.Id;

            #region Asserts
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Content.FirstOrDefault();
                responseGetPet.Should().NotBeNull();
                
                
            }
            #endregion
        }

        [Fact, TestPriority(3)]
        public async Task Update_New_Pet_Test()
        {
            var request = new RestRequest($"/v2/pet", Method.Put);

            GetPetModel monkey = new GetPetModel();

            monkey.Category = new CategoryModel();
            monkey.Category.Name = "Sisi - updated";
            monkey.Name = "Monkey";
            monkey.Status = "pending";

            request.AddJsonBody(monkey);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var reponseContent = JsonConvert.DeserializeObject(response.Content) as JObject;
            var categoryProperty = reponseContent.GetValue("category");
            var categoryResponse = categoryProperty.ToObject<CategoryModel>();

            var responseGetPet = response.Content;

            GetPetModel responsePet = JsonConvert.DeserializeObject<GetPetModel>(response.Content);

            Assert.NotNull(responsePet);
            Assert.NotEqual(0, responsePet.Id);
            Assert.NotNull(responsePet.Name);
            Assert.Equal(monkey.Name, responsePet.Name);
            Assert.NotNull(responsePet.Status);
            Assert.Equal(monkey.Status, responsePet.Status);

            this.TestFixture.Id = responsePet.Id;

            #region Asserts
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Content.FirstOrDefault();
                responseGetPet.Should().NotBeNull();


            }
            #endregion
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
                response.Content.FirstOrDefault();
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
