using System.Collections.Generic;
using System.Threading.Tasks;

namespace BareBones.Messaging.CQRS
{
    public interface IBusClient
    {
        Task PublishAsync<TMessage, TKey>(TKey key, TMessage message, IDictionary<string, object> headers = null) where TMessage : class;
        Task<TReply> SendAsync<TMessage, TKey, TReply>(TKey key, TMessage message, IDictionary<string, object> headers = null) where TMessage : class;
    }
}
