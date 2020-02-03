using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BareBones.Messaging.CQRS;
using Confluent.Kafka;

namespace BareBones.Messaging.Kafka
{
    public class KafkaClient : IBusClient
    {
        private readonly KafkaClientOptions _options;

        public KafkaClient(KafkaClientOptions options)
        {
            _options = options;
        }

        public async Task PublishAsync<TMessage, TKey>(TKey key, TMessage message, IDictionary<string, object> headers = null) where TMessage : class
        {
            using (var producer = new ProducerBuilder<TKey,TMessage>(_options.Configuration).Build())
            {
                await producer.ProduceAsync(
                    _options.TopicProvider.GetTopicFor<TMessage>(),
                    new Message<TKey, TMessage>
                    {
                        Key = key,
                        Value = message,
                        //Headers = ;
                    });
            }
        }

        public Task<TReply> SendAsync<TMessage, TKey, TReply>(TKey key, TMessage message, IDictionary<string, object> headers = null) where TMessage : class
        {
            throw new NotImplementedException();
        }
    }
}
