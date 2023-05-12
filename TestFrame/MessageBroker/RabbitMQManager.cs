using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFrame.MessageBroker
{
    public class RabbitMQManager
    {
        private ConnectionFactory factoryProducer;
        private IConnection connectionProducer;
        private IModel channelProducer;
        private EventingBasicConsumer consumer;

        public RabbitMQManager(IConfiguration config)
        {
            var hostName = config.GetSection("RabbitMQ")["HostName"];
            var port = int.Parse(config.GetSection("RabbitMQ")["Port"]);

            factoryProducer = new ConnectionFactory { HostName = hostName, Port = port };
            connectionProducer = factoryProducer.CreateConnection();
            channelProducer = connectionProducer.CreateModel();

            consumer = new EventingBasicConsumer(channelProducer);
        }

        public void DeclareQueue(string queueName, bool durable, bool exclusive, bool autoDelete, IDictionary<string, object> arguments)
        {
            channelProducer.QueueDeclare(queue: queueName,
                                 durable: durable,
                                 exclusive: exclusive,
                                 autoDelete: autoDelete,
                                 arguments: arguments);
        }

        public void PublishMessage(string exchange, string routingKey, IBasicProperties basicProperties, byte[] body)
        {
            channelProducer.BasicPublish(exchange: exchange,
                                 routingKey: routingKey,
                                 basicProperties: basicProperties,
                                 body: body);
        }

        public void ConsumeMessage(byte[] body, string queueName, bool autoAck)
        {
            consumer.Received += (model, ea) =>
            {
                body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            };
            channelProducer.BasicConsume(queue: queueName, autoAck: autoAck, consumer: consumer);
        }

        public void CloseConnection()
        {
            channelProducer.Close();
            connectionProducer.Close();
        }

        public void StopConsumer()
        {
            channelProducer.BasicCancel(consumer.ConsumerTags[0]);
            consumer.Received -= (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            };
        }

    }
}
