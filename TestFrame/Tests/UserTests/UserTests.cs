using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestFrame.Base;
using TestFrame.Fixtures;
using TestFrame.Models.PetsModels;
using Xunit;
using Xunit.Abstractions;

namespace TestFrame.Tests.UserTests
{
    public class UserTests : OrderedAcceptanceTestsBase<PetsTestFixture>
    {
        public UserTests(PetsTestFixture testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
        {
            var api = config.GetSection("PetsTestData")["PetstoreApiUri"];
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);

            TestFixture.UserName = config.GetSection("PetsTestData")["UserName"];
            TestFixture.Password = config.GetSection("PetsTestData")["Password"];
            TestFixture.UserNameList = config.GetSection("PetsTestData")["UserNameList"];
            TestFixture.PasswordList = config.GetSection("PetsTestData")["PasswordList"];
            TestFixture.InvalidUserName = config.GetSection("PetsTestData")["InvalidUserName"];
            TestFixture.InvalidPassword = config.GetSection("PetsTestData")["InvalidPassword"];
        }
        [Fact, TestPriority(1)]
        public async Task Create_A_User_Test()
        {
            var request = new RestRequest($"/v2/user", Method.Post);

            User user = new User();

            user.Username = TestFixture.UserName;
            user.FirstName = "Testing";
            user.LastName = "PetShop";
            user.Password = TestFixture.Password;
            user.Phone = "123456789";
            user.UserStatus = 1;

            request.AddBody(user);

            var response = await TestFixture.Client.ExecuteAsync(request);

            ResponsePetStore responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseUser);
            Assert.Equal(200, responseUser.Code);
            Assert.NotNull(responseUser.Message);





        }
        [Fact, TestPriority(2)]
        public async Task Update_User_ByUserName_Test()
        {
            var request = new RestRequest($"/v2/user/{TestFixture.UserName}", Method.Put);

            User user = new User();

            user.FirstName = "Test";
            user.LastName = "PetShop updated"; ;
            user.Phone = "123456799";

            request.AddBody(user);

            var response = await TestFixture.Client.ExecuteAsync(request);

            ResponsePetStore responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseUser);
            Assert.Equal(200, responseUser.Code);
            Assert.NotNull(responseUser.Message);



        }
        [Fact, TestPriority(3)]
        public async Task Get_User_ByUsername_Test()
        {
            var request = new RestRequest($"/v2/user/{TestFixture.UserName}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            User responseUser = JsonConvert.DeserializeObject<User>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseUser);

            Assert.NotEqual(0, responseUser.Id);
            Assert.NotNull(responseUser.Username);
            Assert.Equal(TestFixture.UserName, responseUser.Username);
            Assert.Equal(TestFixture.Password, responseUser.Password);
            Assert.NotNull(responseUser.FirstName);
            Assert.NotNull(responseUser.LastName);
            Assert.NotNull(responseUser.Phone);
            Assert.NotEqual(0, responseUser.UserStatus);

        }

        [Fact, TestPriority(4)]
        public async Task Login_With_User_Test()
        {
            var request = new RestRequest($"/v2/user/login?username='{TestFixture.UserName}'&password='{TestFixture.Password}'", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            ResponsePetStore responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseUser);
            Assert.Equal(200, responseUser.Code);
            Assert.NotNull(responseUser.Message);

        }

        [Fact, TestPriority(5)]
        public async Task Logout_With_User_Test()
        {
            var request = new RestRequest($"/v2/user/logout", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            ResponsePetStore responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseUser);
            Assert.Equal(200, responseUser.Code);
            Assert.NotNull(responseUser.Message);

        }

        [Fact, TestPriority(6)]
        public async Task Delere_User_ByUserName_Test()
        {
            var request = new RestRequest($"v2/user/{TestFixture.UserName}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);


            ResponsePetStore responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseUser);
            Assert.Equal(200, responseUser.Code);
            Assert.NotNull(responseUser.Message);

        }

        [Fact, TestPriority(7)]
        public async Task Create_User_With_Array_Test()
        {
            var request = new RestRequest($"v2/user/createWithArray", Method.Post);

            List<User> list = new List<User>();

            User user = new User();
            user.Username = TestFixture.UserNameList;
            user.Password = TestFixture.PasswordList;
            user.FirstName = "testing";
            user.LastName = "testing";
            user.Phone = "0123456789";
            user.Email = "test@mail.com";
            user.UserStatus = 1;

            list.Add(user);
            request.AddBody(list);

            var response = await TestFixture.Client.ExecuteAsync(request);

            ResponsePetStore responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseUser);
            Assert.Equal(200, responseUser.Code);
            Assert.NotNull(responseUser.Message);


        }

        [Fact, TestPriority(8)]
        public async Task Delere_User_ForCleaningDataCreated_Test()
        {
            var request = new RestRequest($"v2/user/{TestFixture.UserNameList}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);


            ResponsePetStore responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseUser);
            Assert.Equal(200, responseUser.Code);
            Assert.NotNull(responseUser.Message);

        }

        [Fact, TestPriority(9)]
        public async Task Create_User_With_List_Test()
        {
            var request = new RestRequest($"v2/user/createWithList", Method.Post);

            List<User> list = new List<User>();

            User user = new User();
            user.Username = TestFixture.UserNameList;
            user.Password = TestFixture.PasswordList;
            user.FirstName = "testing";
            user.LastName = "testing";
            user.Phone = "0123456789";
            user.Email = "test@mail.com";
            user.UserStatus = 1;

            list.Add(user);
            request.AddBody(list);

            var response = await TestFixture.Client.ExecuteAsync(request);

            ResponsePetStore responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseUser);
            Assert.Equal(200, responseUser.Code);
            Assert.NotNull(responseUser.Message);


        }

        [Fact, TestPriority(10)]
        public async Task Delere_User_ForCleaningDataCreated2_Test()
        {
            var request = new RestRequest($"v2/user/{TestFixture.UserNameList}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);


            ResponsePetStore responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseUser);
            Assert.Equal(200, responseUser.Code);
            Assert.NotNull(responseUser.Message);

        }


        //Negative tests

        [Fact, TestPriority(11)]
        public async Task Get_User_ByUsername_Negative_Test()
        {
            var request = new RestRequest($"/v2/user/{TestFixture.InvalidUserName}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            ResponsePetStore responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(responseUser);
            Assert.Equal("User not found",responseUser.Message);

        }
  

        [Fact, TestPriority(12)]
        public async Task Delere_User_ByUserName_Negative_Test()
        {
            var request = new RestRequest($"v2/user/{TestFixture.InvalidUserName}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }


    }
}
