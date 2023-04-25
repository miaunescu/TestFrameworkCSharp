using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace TestFrame.Tests.StoreTests
{
    public class StoreTests : OrderedAcceptanceTestsBase<PetsTestFixture>
    {
        public StoreTests(PetsTestFixture testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
        {
            var api = config.GetSection("PetsTestData")["PetstoreApiUri"];
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
            TestFixture.PetID = int.Parse(config.GetSection("PetsTestData")["PetID"]);
        }

        [Fact, TestPriority(1)]
        public async Task Place_An_Order_For_APet_Test()
        {
            var request = new RestRequest($"/v2/store/order", Method.Post);

            Order order = new Order();
            order.PetId = TestFixture.PetID;
            order.Quantity = 1;
            order.ShipDate = DateTime.UtcNow.Date ;
            order.Status = "placed";
            order.Complete = true;

            request.AddBody(order);

            var response = await TestFixture.Client.ExecuteAsync(request);

            Order responseOrder = JsonConvert.DeserializeObject<Order>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseOrder);
            Assert.NotEqual(0,responseOrder.Id);
            Assert.NotEqual(0,responseOrder.PetId);
            Assert.Equal(order.PetId, responseOrder.PetId);
            Assert.Equal(order.Quantity, responseOrder.Quantity);
            Assert.NotNull(responseOrder.Status);
            Assert.Equal(order.Status, responseOrder.Status);
            Assert.Equal(order.Complete, responseOrder.Complete);

            this.TestFixture.OrderId = responseOrder.Id;


        }

        [Fact, TestPriority(2)]
        public async Task Get_Order_By_Id_Test()
        {
            var request = new RestRequest($"/v2/store/order/{TestFixture.OrderId}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            Order responseOrder = JsonConvert.DeserializeObject<Order>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseOrder);
            Assert.Equal(TestFixture.OrderId, responseOrder.Id);
            Assert.NotNull(responseOrder.Status);
            Assert.NotEqual(0, responseOrder.PetId);
            Assert.NotEqual(0, responseOrder.Quantity);
            

        }

        [Fact, TestPriority(3)]
        public async Task Delete_Order_By_Id_Test()
        {
            var request = new RestRequest($"/v2/store/order/{TestFixture.OrderId}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);

            ResponsePetStore responseOrder = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseOrder);
            Assert.NotEqual(0,responseOrder.Code);
            Assert.NotNull(responseOrder.Message);
        }

        [Fact, TestPriority(4)]
        public async Task Get_Store_Inventory_Test()
        {
            var request = new RestRequest($"/v2/store/inventory", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response);
           

        }

        //negative tests

        [Theory, TestPriority(5)]
        [InlineData(0000)]
        [InlineData(97)]
        public async Task Get_Order_By_Id_Negative_Test(int order)        
       {
            var request = new RestRequest($"/v2/store/order/{order}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            ResponsePetStore responseOrder = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(responseOrder);
            Assert.Equal("Order not found", responseOrder.Message);

        }

        [Theory, TestPriority(6)]
        [InlineData(0000)]
        [InlineData(97)]
        public async Task Delete_Order_By_Id_Negative_Test(int order)
        {
            var request = new RestRequest($"/v2/store/order/{order}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);

            ResponsePetStore responseOrder = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(responseOrder);
            Assert.Equal("Order Not Found", responseOrder.Message);

        }


    }
}
