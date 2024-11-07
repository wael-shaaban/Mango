using Mango.Services.RewardsApi.Messaging;
using Mango.Services.RewardsApi.Models;

namespace Mango.Services.EmailApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMQConsumerService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));
            services.AddSingleton<IRabbitMqServiceBusConsumer, RabbitMqServiceBusConsumer>();
            return services;
        }
    }
}