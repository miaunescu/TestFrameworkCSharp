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

namespace TestFrame.Tests.StoreTests
{
    public class StoreTests : OrderedAcceptanceTestsBase<PetsTestFixture>
    {
        private const string _petsTestData = "PetsTestData";
        public StoreTests(PetsTestFixture testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
        {
            var api = config.GetSection(_petsTestData)["PetstoreApiUri"];
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
            TestFixture.PetID = int.Parse(config.GetSection(_petsTestData)["PetID"]!);
        }

        [Fact, TestPriority(1)]
        public async Task Place_An_Order_For_APet_Test()
        {
            var request = new RestRequest($"/v2/store/order", Method.Post);

            var order = new Order()
            {
                PetId = TestFixture.PetID,
                Quantity = 1,
                ShipDate = DateTime.UtcNow.Date,
                Status = "placed",
                Complete = true
            };

            request.AddBody(order);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseOrder = JsonConvert.DeserializeObject<Order>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                responseOrder.Should().NotBeNull();
                responseOrder.Status.Should().NotBeNull();
                responseOrder.Id.Should().BeGreaterThan(0);
                responseOrder.PetId.Should().BeGreaterThan(0);
                responseOrder.PetId.Should().Be(order.PetId);
                responseOrder.Quantity.Should().Be(order.Quantity);
                responseOrder.Status.Should().Be(order.Status);
                responseOrder.Complete.Should().Be(order.Complete);
            }
            TestFixture.OrderId = responseOrder.Id;
        }

        [Fact, TestPriority(2)]
        public async Task Get_Order_By_Id_Test()
        {
            var request = new RestRequest($"/v2/store/order/{TestFixture.OrderId}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseOrder = JsonConvert.DeserializeObject<Order>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                responseOrder.Should().NotBeNull();
                responseOrder.Status.Should().NotBeNull();
                responseOrder.PetId.Should().BeGreaterThan(0);
                responseOrder.Quantity.Should().BeGreaterThan(0);
                responseOrder.Id.Should().Be(TestFixture.OrderId);
            }
        }

        [Fact, TestPriority(3)]
        public async Task Delete_Order_By_Id_Test()
        {
            var request = new RestRequest($"/v2/store/order/{TestFixture.OrderId}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseOrder = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                responseOrder.Should().NotBeNull();
                responseOrder.Message.Should().NotBeNull();
                responseOrder.Code.Should().BeGreaterThan(0);
            }
        }

        [Fact, TestPriority(4)]
        public async Task Get_Store_Inventory_Test()
        {
            var request = new RestRequest($"/v2/store/inventory", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();
            }
        }

        //negative tests
        [Theory, TestPriority(5)]
        [InlineData(0000)]
        [InlineData(97)]
        public async Task Get_Order_By_Id_Negative_Test(int order)
        {
            var request = new RestRequest($"/v2/store/order/{order}", Method.Get);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseOrder = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);

                responseOrder.Should().NotBeNull();
                responseOrder.Message.Should().NotBeNull().And.Be("Order not found");
            }
        }

        [Theory, TestPriority(6)]
        [InlineData(0000)]
        [InlineData(97)]
        public async Task Delete_Order_By_Id_Negative_Test(int order)
        {
            var request = new RestRequest($"/v2/store/order/{order}", Method.Delete);

            var response = await TestFixture.Client.ExecuteAsync(request);

            var responseOrder = JsonConvert.DeserializeObject<ResponsePetStore>(response.Content);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.NotFound);

                responseOrder.Should().NotBeNull();
                responseOrder.Message.Should().Be("Order Not Found");
            }
        }
    }
}
