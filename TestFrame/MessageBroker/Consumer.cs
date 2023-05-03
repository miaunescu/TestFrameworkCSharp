using System;
using System.Threading.Tasks;
using MassTransit;
using Newtonsoft.Json;
using RestSharp;

namespace ConsumerApp
{
    public class CountryModel
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public int Population { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://35.158.22.93"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("RestCountries", e =>
                {
                    e.Handler<CountryModel>(async context =>
                    {
                        var country = context.Message;
                        var client = new RestClient($"https://restcountries.com/v3.1/name/{country.Name}");
                        var request = new RestRequest(Method.GET);
                        var response = await client.ExecuteAsync(request);
                        var content = response.Content;
                        var details = JsonConvert.DeserializeObject<CountryModel[]>(content);

                        Console.WriteLine($"Name: {details[0].Name}");
                        Console.WriteLine($"Capital: {details[0].Capital}");
                        Console.WriteLine($"Region: {details[0].Region}");
                        Console.WriteLine($"Subregion: {details[0].Subregion}");
                        Console.WriteLine($"Population: {details[0].Population}");
                        Console.WriteLine();
                    });
                });
            });

            await bus.StartAsync();

            Console.WriteLine("Listening for messages.. Press any key to exit");
            Console.ReadKey();

            await bus.StopAsync();
        }
    }
}
