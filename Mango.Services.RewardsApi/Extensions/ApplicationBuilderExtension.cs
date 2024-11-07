using Mango.Services.RewardsApi.Messaging;

namespace Mango.Services.EmailApi.Extensions
{
    public static class ApplicationBuilderExtension
    {
        private static IRabbitMqServiceBusConsumer RabbitMqServiceBusConsumer { get; set; }
        public static IApplicationBuilder UseRabbitMqServiceConsumer(this IApplicationBuilder app)
        {
            RabbitMqServiceBusConsumer = app.ApplicationServices.GetService<IRabbitMqServiceBusConsumer>();
            var hostAppLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            hostAppLifetime.ApplicationStarted.Register(OnStart);
            hostAppLifetime.ApplicationStopping.Register(OnStop);
            return app;
        }

        private static void OnStop()
        {
           RabbitMqServiceBusConsumer.Stop();   
        }

        private static void OnStart()
        {
            RabbitMqServiceBusConsumer.Start();
        }
    }
}
