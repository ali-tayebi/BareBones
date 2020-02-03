using Microsoft.Extensions.DependencyInjection;

namespace BareBones.Messaging.Outbox
{
    public interface IMessageOutboxBuilder
    {
        IBareBonesBuilder Builder { get; }
        OutboxOptions Options { get; }
    }

    internal sealed class MessageOutboxBuilder : IMessageOutboxBuilder
    {
        public IBareBonesBuilder Builder { get; }
        public OutboxOptions Options { get; }

        public MessageOutboxBuilder(IBareBonesBuilder builder, OutboxOptions options)
        {
            Builder = builder;
            Options = options;
        }
    }
}
