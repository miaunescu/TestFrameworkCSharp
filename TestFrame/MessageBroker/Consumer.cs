using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factoryConsumer = new ConnectionFactory { HostName = "35.158.22.93", Port = 5672 };
using var connectionConsumer = factoryConsumer.CreateConnection();
using var channelConsumer = connectionConsumer.CreateModel();

channelConsumer.QueueDeclare(queue: "RestCountries",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channelConsumer);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
};
channelConsumer.BasicConsume(queue: "RestCountries",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();



