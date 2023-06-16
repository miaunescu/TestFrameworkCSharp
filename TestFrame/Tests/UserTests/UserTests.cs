using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TestFrame.Base;
using TestFrame.Fixtures;
using TestFrame.Models.PetsModels;
using Xunit;
using Xunit.Abstractions;

namespace TestFrame.Tests.UserTests
{
    public class UserTests : OrderedAcceptanceTestsBase<PetsTestFixture>
    {
        private const string _petsTestData = "PetsTestData";
        public UserTests(PetsTestFixture testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
        {
            var api = config.GetSection(_petsTestData)["PetstoreApiUri"];
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);

            TestFixture.UserName = config.GetSection(_petsTestData)["UserName"];
            TestFixture.Password = config.GetSection(_petsTestData)["Password"];
            TestFixture.UserNameList = config.GetSection(_petsTestData)["UserNameList"];
            TestFixture.PasswordList = config.GetSection(_petsTestData)["PasswordList"];
            TestFixture.InvalidUserName = config.GetSection(_petsTestData)["InvalidUserName"];
            TestFixture.InvalidPassword = config.GetSection(_petsTestData)["InvalidPassword"];
        }

        [Fact, TestPriority(1)]
        public async Task Create_A_User_Test()
        {
            var request = new RestRequest($"/v2/user", Method.Post);

            var user = new User()
            {
                Username = TestFixture.UserName,
                FirstName = "Testing",
                LastName = "PetShop",
                Password = TestFixture.Password,
                Phone = "123456789",
                UserStatus = 1
            };

            request.AddBody(user);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                responseUser.Should().NotBeNull();
                responseUser.Message.Should().NotBeNull();
                responseUser.Code.Should().Be(200);
            }
        }

        [Fact, TestPriority(2)]
        public async Task Update_User_ByUserName_Test()
        {
            var request = new RestRequest($"/v2/user/{TestFixture.UserName}", Method.Put);

            var user = new User()
            {
                FirstName = "Test",
                LastName = "PetShop updated",
                Phone = "123456799"
            };

            request.AddBody(user);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();
                responseUser.Should().NotBeNull();
                responseUser.Message.Should().NotBeNull();
                responseUser.Code.Should().Be(200);
            }
        }

        [Fact, TestPriority(3)]
        public async Task Get_User_ByUsername_Test()
        {
            var request = new RestRequest($"/v2/user/{TestFixture.UserName}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseUser = JsonConvert.DeserializeObject<User>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                responseUser.Should().NotBeNull();
                responseUser.Username.Should().NotBeNull();
                responseUser.FirstName.Should().NotBeNull();
                responseUser.LastName.Should().NotBeNull();
                responseUser.Phone.Should().NotBeNull();
                responseUser.Id.Should().BeGreaterThan(0);
                responseUser.UserStatus.Should().BeGreaterThan(0);

                responseUser.Username.Should().Be(TestFixture.UserName);
                responseUser.Password.Should().Be(TestFixture.Password);
            }
        }

        [Fact, TestPriority(4)]
        public async Task Login_With_User_Test()
        {
            var request = new RestRequest($"/v2/user/login?username='{TestFixture.UserName}'&password='{TestFixture.Password}'", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                responseUser.Should().NotBeNull();
                responseUser.Message.Should().NotBeNull();
                responseUser.Code.Should().Be(200);
            }
        }

        [Fact, TestPriority(5)]
        public async Task Logout_With_User_Test()
        {
            var request = new RestRequest($"/v2/user/logout", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                responseUser.Should().NotBeNull();
                responseUser.Message.Should().NotBeNull();
                responseUser.Code.Should().Be(200);
            }
        }

        [Fact, TestPriority(6)]
        public async Task Delete_User_ByUserName_Test()
        {
            var request = new RestRequest($"v2/user/{TestFixture.UserName}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                responseUser.Should().NotBeNull();
                responseUser.Message.Should().NotBeNull();
                responseUser.Code.Should().Be(200);
            }
        }

        [Fact, TestPriority(7)]
        public async Task Create_User_With_Array_Test()
        {
            var request = new RestRequest($"v2/user/createWithArray", Method.Post);

            var list = new List<User>();

            var user = new User()
            {
                Username = TestFixture.UserNameList,
                Password = TestFixture.PasswordList,
                FirstName = "testing",
                LastName = "testing",
                Phone = "0123456789",
                Email = "test@mail.com",
                UserStatus = 1
            };

            list.Add(user);
            request.AddBody(list);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                responseUser.Should().NotBeNull();
                responseUser.Message.Should().NotBeNull();
                responseUser.Code.Should().Be(200);
            }
        }

        [Fact, TestPriority(8)]
        public async Task Delete_User_ForCleaningDataCreated_Test()
        {
            var request = new RestRequest($"v2/user/{TestFixture.UserNameList}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                responseUser.Should().NotBeNull();
                responseUser.Message.Should().NotBeNull();
                responseUser.Code.Should().Be(200);
            }
        }

        [Fact, TestPriority(9)]
        public async Task Create_User_With_List_Test()
        {
            var request = new RestRequest($"v2/user/createWithList", Method.Post);

            var list = new List<User>();

            var user = new User()
            {
                Username = TestFixture.UserNameList,
                Password = TestFixture.PasswordList,
                FirstName = "testing",
                LastName = "testing",
                Phone = "0123456789",
                Email = "test@mail.com",
                UserStatus = 1
            };

            list.Add(user);
            request.AddBody(list);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                responseUser.Should().NotBeNull();
                responseUser.Message.Should().NotBeNull();
                responseUser.Code.Should().Be(200);
            }
        }

        [Fact, TestPriority(10)]
        public async Task Delete_User_ForCleaningDataCreated2_Test()
        {
            var request = new RestRequest($"v2/user/{TestFixture.UserNameList}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                responseUser.Should().NotBeNull();
                responseUser.Message.Should().NotBeNull();
                responseUser.Code.Should().Be(200);
            }
        }

        //Negative tests
        [Fact, TestPriority(11)]
        public async Task Get_User_ByUsername_Negative_Test()
        {
            var request = new RestRequest($"/v2/user/{TestFixture.InvalidUserName}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseUser = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
                response.Should().NotBeNull();

                responseUser.Should().NotBeNull();
                responseUser.Message.Should().Be("User not found");
            }
        }

        [Fact, TestPriority(12)]
        public async Task Delete_User_ByUserName_Negative_Test()
        {
            var request = new RestRequest($"v2/user/{TestFixture.InvalidUserName}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}
