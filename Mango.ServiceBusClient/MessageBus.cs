using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.ServiceBusClient
{
    internal class MessageBus : IMessageBus, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task PublishMessage(object? message, string queue_topic_name)
        {
            throw new NotImplementedException();
        }
    }
}
