using System.Collections.Generic;
using System.Threading.Tasks;

namespace BareBones.Messaging.Outbox
{
    public interface IMessageOutboxRepository
    {
        Task<IReadOnlyList<OutboxMessage>> GetUnsentAsync();
        Task ProcessAsync(OutboxMessage message);
        Task ProcessAsync(IEnumerable<OutboxMessage> outboxMessages);
    }
}
