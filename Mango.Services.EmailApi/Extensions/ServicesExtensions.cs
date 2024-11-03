using Mango.Services.EmailApi.Messaging;
using Mango.Services.EmailApi.Models;
using Microsoft.Extensions.Options;

namespace Mango.Services.EmailApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMQConsumerService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));
            services.AddSingleton<IRabbitMqServiceBusConsumer,RabbitMqServiceBusConsumer>();
            return services;
        }
    }
}