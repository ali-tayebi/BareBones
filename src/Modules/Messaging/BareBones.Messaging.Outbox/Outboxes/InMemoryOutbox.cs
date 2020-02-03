using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BareBones.Messaging.Outbox.Outboxes
{
      internal sealed class InMemoryMessageOutbox : IMessageOutbox, IMessageOutboxRepository
    {
        private readonly ConcurrentDictionary<string, bool> _inboxMessages =
            new ConcurrentDictionary<string, bool>();

        private readonly ConcurrentDictionary<string, OutboxMessage> _outboxMessages =
            new ConcurrentDictionary<string, OutboxMessage>();

        private readonly ILogger<InMemoryMessageOutbox> _logger;
        private readonly int _expiry;

        public InMemoryMessageOutbox(OutboxOptions options, ILogger<InMemoryMessageOutbox> logger)
        {
            _logger = logger;
            _expiry = options.Expiry;
            Enabled = options.Enabled;
        }

        public bool Enabled { get; }

        public async Task HandleAsync(string messageId, Func<Task> handler)
        {
            if (!Enabled)
            {
                _logger.LogWarning("Outbox is disabled, incoming messages won't be processed.");
                return;
            }

            if (string.IsNullOrWhiteSpace(messageId))
            {
                throw new ArgumentException("Message id to be processed cannot be empty.", nameof(messageId));
            }

            _logger.LogTrace($"Received a message with id: '{messageId}' to be processed.");
            if (_inboxMessages.ContainsKey(messageId))
            {
                _logger.LogTrace($"Message with id: '{messageId}' was already processed.");
                return;
            }

            _logger.LogTrace($"Processing a message with id: '{messageId}'...");
            await handler();
            if (!_inboxMessages.TryAdd(messageId, true))
            {
                _logger.LogError($"There was an error when processing a message with id: '{messageId}'.");

                throw new InvalidOperationException($"Couldn't add a processed message with id: '{messageId}'" +
                                                    "to the internal dictionary.");
            }

            _logger.LogTrace($"Processed a message with id: '{messageId}'.");
        }

        public Task SendAsync<T>(T message, string originatedMessageId = null, string messageId = null,
            string correlationId = null, string spanContext = null, object messageContext = null,
            IDictionary<string, object> headers = null) where T : class
        {
            if (!Enabled)
            {
                _logger.LogWarning("Outbox is disabled, messages won't be saved into the storage.");
                return Task.CompletedTask;
            }

            var outboxMessage = new OutboxMessage
            {
                IdentityKey = string.IsNullOrWhiteSpace(messageId) ? Guid.NewGuid().ToString("N") : messageId,
                OriginatedMessageId = originatedMessageId,
                CorrelationId = correlationId,
                SpanContext = spanContext,
                MessageContextType = messageContext?.GetType().AssemblyQualifiedName,
                Headers = (Dictionary<string, object>) headers,
                Message = message,
                MessageContext = messageContext,
                MessageType = message?.GetType().AssemblyQualifiedName,
                SentAt = DateTime.UtcNow
            };
            _outboxMessages.TryAdd(outboxMessage.IdentityKey, outboxMessage);

            return Task.CompletedTask;
        }

        Task<IReadOnlyList<OutboxMessage>> IMessageOutboxRepository.GetUnsentAsync()
            => Task.FromResult<IReadOnlyList<OutboxMessage>>(_outboxMessages.Values
                .Where(m => m.ProcessedAt is null)
                .ToList());

        Task IMessageOutboxRepository.ProcessAsync(IEnumerable<OutboxMessage> outboxMessages)
        {
            foreach (var message in outboxMessages)
            {
                message.ProcessedAt = DateTime.UtcNow;
            }
            RemoveExpiredMessages();

            return Task.CompletedTask;
        }

        Task IMessageOutboxRepository.ProcessAsync(OutboxMessage message)
        {
            message.ProcessedAt = DateTime.UtcNow;
            RemoveExpiredMessages();

            return Task.CompletedTask;
        }

        private void RemoveExpiredMessages()
        {
            if (_expiry <= 0)
            {
                return;
            }

            foreach (var msg in _outboxMessages)
            {
                if (!msg.Value.ProcessedAt.HasValue)
                {
                    continue;
                }

                if (msg.Value.ProcessedAt.Value.AddSeconds(_expiry) > DateTime.UtcNow)
                {
                    continue;
                }

                _outboxMessages.TryRemove(msg.Key, out _);
                _inboxMessages.TryRemove(msg.Value.OriginatedMessageId, out _);
            }
        }
    }
}
