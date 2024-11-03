
namespace Mango.Services.EmailApi.Messaging
{
    public interface IRabbitMqServiceBusConsumer
    {
        void Dispose();
        //void StartConsuming<T>(Action<T> processMessage);
        Task Stop();
        Task Start();
    }
}