using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.ServiceBusClient
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMQService(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the RabbitMQ settings
            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));
            // Register RabbitMQService
            services.AddSingleton<IRabbitMQService,RabbitMQService>();

            return services;
        }
    }
}