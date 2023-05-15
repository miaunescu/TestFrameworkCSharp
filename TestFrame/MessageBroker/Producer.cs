//using System.Text;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
//using RabbitMQ.Client;
//using TestFrame;
//using TestFrame.Base;
//using TestFrame.Builder;
//using TestFrame.Fixtures;


//var factoryProducer = new ConnectionFactory { HostName = "35.158.22.93", Port = 5672 };
//using var connectionProducer = factoryProducer.CreateConnection();
//using var channelProducer = connectionProducer.CreateModel();

//channelProducer.QueueDeclare(queue: "RestCountries",
//                     durable: false,
//                     exclusive: false,
//                     autoDelete: false,
//                     arguments: null);

//const string message = "Romania";
//var body = Encoding.UTF8.GetBytes(message);

//channelProducer.BasicPublish(exchange: string.Empty,
//                     routingKey: "RestCountries",
//                     basicProperties: null,
//                     body: body);
//Console.WriteLine($" [x] Sent {message}");

//Console.WriteLine(" Press [enter] to exit.");
//Console.ReadLine();