using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace Mango.ServiceBusClient
{
    public class RabbitMQService : IDisposable, IRabbitMQService
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly RabbitMQSettings _settings;

        public RabbitMQService(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;

            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password
              //  ,ClientProvidedName = _settings.ClientProvidedName
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _settings.ExchangeName, type: ExchangeType.Direct);
            _channel.QueueDeclare(queue: _settings.QueueName, durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue: _settings.QueueName, exchange: _settings.ExchangeName, routingKey: _settings.RoutingKey);
        }

        public void PublishMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: _settings.ExchangeName, routingKey: _settings.RoutingKey, basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel?.Close();
            //_connection?.Close();
        }
    }
}
