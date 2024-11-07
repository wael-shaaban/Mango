namespace Mango.Services.RewardsApi.Messaging
{
    public interface IRabbitMqServiceBusConsumer
    {
        void Dispose();
        //void StartConsuming<T>(Action<T> processMessage);
        Task Stop();
        Task Start();
    }
}