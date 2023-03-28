//using Newtonsoft.Json;
//using RestSharp;
//using RestSharp.Authenticators;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TestFrame.Base;
//using TestFrame.Builder;
//using TestFrame.Fixtures;
//using TestFrame.Models;
//using Xunit;
//using Xunit.Abstractions;

//namespace TestFrame.Tests
//{
//    public class PetsTests : OrderedAcceptanceTestsBase<PetsTestFixture>
//    {
//        public PetsTests(PetsTestFixture testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
//        {
//            var api = config.GetSection("TestData")["PetstoreApiUri"];
//            TestFixture.Api = api;
//            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
//            TestFixture.PetID = int.Parse(config.GetSection("TestData")["PetID"]);
//        }

//        [FactSequence(1)]
//        public async Task GetPetById()
//        {
//            var request = new RestRequest($"/v2/pet/20", Method.Get);

//            var response = await TestFixture.Client.ExecuteAsync(request);

//            var responseDe = JsonConvert.DeserializeObject<GetPetModel>(response.Content);

//            Assert.True(responseDe != null);

//            output.WriteLine("This is test 1");

//            Thread.Sleep(2000);
//        }

//        [FactSequence(2)]
//        public async Task GetPetById2()
//        {
//            var request = new RestRequest($"/v2/pet/20", Method.Get);

//            var response = await TestFixture.Client.ExecuteAsync(request);

//            var responseDe = JsonConvert.DeserializeObject<GetPetModel>(response.Content);

//            Assert.True(responseDe != null);

//            output.WriteLine("This is test 2");
//            Thread.Sleep(2000);

//        }

//        [FactSequence(3)]
//        public async Task GetPetById3()
//        {
//            var request = new RestRequest($"/v2/pet/20", Method.Get);

//            var response = await TestFixture.Client.ExecuteAsync<GetPetModel>(request);

//            Assert.True(response != null);

//            output.WriteLine("This is test 3");
//            Thread.Sleep(2000);

//        }

//        [FactSequence(4)]
//        public async Task GetPetById4()
//        {
//            var request = new RestRequest($"/v2/pet/20", Method.Get);

//            var response = await TestFixture.Client.ExecuteAsync(request);

//            var responseDe = JsonConvert.DeserializeObject<GetPetModel>(response.Content);

//            Assert.True(responseDe != null);

//            output.WriteLine("This is test 4");
//            Thread.Sleep(2000);

//        }

//        [FactSequence(5)]
//        public async Task GetPetById5()
//        {
//            var request = new RestRequest($"/v2/pet/20", Method.Get);

//            var response = await TestFixture.Client.ExecuteAsync(request);

//            var responseDe = JsonConvert.DeserializeObject<GetPetModel>(response.Content);

//            Assert.True(responseDe != null);

//            output.WriteLine("This is test 5");

//        }
//    }
//}
