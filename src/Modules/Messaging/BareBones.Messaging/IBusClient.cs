using System.Collections.Generic;
using System.Threading.Tasks;

namespace BareBones.Messaging.CQRS
{
    public interface IBusClient : IBusPublisher
    {
        Task<TReply> SendAsync<TMessage, TKey, TReply>(TKey key, TMessage message, IDictionary<string, object> headers = null) where TMessage : class;
    }

    public interface IBusPublisher
    {
        Task PublishAsync<TMessage, TKey>(TKey key, TMessage message, IDictionary<string, object> headers = null) where TMessage : class;
    }
}
