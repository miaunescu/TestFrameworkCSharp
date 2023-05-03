//using System.Text;
//using RabbitMQ.Client;

//var factory = new ConnectionFactory { HostName = "35.158.22.93", Port=5672 };
//using var connection = factory.CreateConnection();
//using var channel = connection.CreateModel();

//channel.QueueDeclare(queue: "RestCountries",
//                     durable: false,
//                     exclusive: false,
//                     autoDelete: false,
//                     arguments: null);

//const string message = "romania";

//var body = Encoding.UTF8.GetBytes(message);

//channel.BasicPublish(exchange: string.Empty,
//                     routingKey: "RestCountries",
//                     basicProperties: null,
//                     body: body);
//Console.WriteLine($" [x] Sent {message}");

//Console.WriteLine(" Press [enter] to exit.");
//Console.ReadLine();



//===============================================================
//===============================================================
//===============================================================

using System.Text;
using MassTransit;
using RabbitMQ.Client;
using RestSharp;

var factory = new ConnectionFactory { HostName = "35.158.22.93", Port = 5672 };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "RestCountries",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var message = GetMessage(args);
var body = Encoding.UTF8.GetBytes(message);

var properties = channel.CreateBasicProperties();
properties.Persistent = true;

channel.BasicPublish(exchange: string.Empty,
                     routingKey: "RestCountries",
                     basicProperties: properties,
                     body: body);
Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

static string GetMessage(string[] args)
{
    return ((args.Length > 0) ? string.Join(" ", args) : "Romania!");
}


//===============================================================
//===============================================================
//===============================================================


