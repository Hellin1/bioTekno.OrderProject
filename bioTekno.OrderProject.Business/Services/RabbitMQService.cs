using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Business.Services
{
    public class RabbitMQService
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;

        public RabbitMQService(string hostName, string queueName)
        {
            _factory = new ConnectionFactory() { HostName = hostName };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queueName,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
            _queueName = queueName;
        }

        public void Listen(Action<string> handleMessage)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                handleMessage(message);
            };
            _channel.BasicConsume(queue: _queueName,
                                  autoAck: true,
                                  consumer: consumer);
        }

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                                  routingKey: _queueName,
                                  basicProperties: null,
                                  body: body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
