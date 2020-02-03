using System.Collections.Generic;

namespace BareBones.Messaging.Kafka
{
    public class KafkaClientOptions
    {
        public IDictionary<string, string> Configuration { get; set; }
        public ITopicProvider TopicProvider { get; set; }
    }
}
