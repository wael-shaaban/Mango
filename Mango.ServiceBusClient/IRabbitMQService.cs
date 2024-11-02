namespace Mango.ServiceBusClient
{
    public interface IRabbitMQService
    {
        void Dispose();
        void PublishMessage(string message);
    }
}