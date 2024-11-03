using Mango.Services.EmailApi.Dtos;
using Mango.Services.EmailApi.Models;
using Mango.Services.EmailApi.Services;
using Mango.Services.EmailApi.Services.IServices;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime;
using System.Text;
using System.Text.Json;

namespace Mango.Services.EmailApi.Messaging
{
    public class RabbitMqServiceBusConsumer : IRabbitMqServiceBusConsumer
    {
        private readonly RabbitMQSettings settings;
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly string _queueName;
        private readonly EmailService emailService;
        private string _consumerCancelTag;
        public RabbitMqServiceBusConsumer(IOptions<RabbitMQSettings> rabbitmqSettings,EmailService emailService)
        {
            settings = rabbitmqSettings.Value;     
            this.emailService = emailService;

            var factory = new ConnectionFactory
            {
                HostName = settings.HostName,
                Port = settings.Port,
                UserName = settings.UserName,
                Password = settings.Password,
                ClientProvidedName = settings.ClientProvidedName
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = settings.QueueName;

            _channel.ExchangeDeclare(exchange: settings.ExchangeName, type: ExchangeType.Direct);
            _channel.QueueDeclare(queue: settings.QueueName, durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue: settings.QueueName, exchange: settings.ExchangeName, routingKey: settings.RoutingKey);
            _channel.BasicQos(0, 1, false);
        }

        public async Task Start()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, eventArgs) =>
            {
                // Receive message body
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try//hellow world
                {
                    // Deserialize the message to CartDto
                    var objMessage = JsonConvert.DeserializeObject<CartDto>(message);

                    // TODO: Process the deserialized object, e.g., log or handle business logic
                    Console.WriteLine($"Received message: {objMessage}");
                    emailService.EmailCartAndLog(objMessage);
                    // Acknowledge message
                    _channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                    // Optionally, reject the message and requeue it
                    _channel.BasicNack(deliveryTag: eventArgs.DeliveryTag, multiple: false, requeue: true);
                }
            };

            // Start consuming messages
           _consumerCancelTag =  _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }
       
        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }

        public async Task Stop()
        {
            _channel.BasicCancel(_consumerCancelTag);
        }
    }
}
