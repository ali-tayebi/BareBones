namespace BareBones.Messaging.Kafka
{
    public interface ITopicProvider
    {
        string GetTopicFor<TMessage>();
    }

    public class ClassAttributeTopicProvider : ITopicProvider
    {
        public string GetTopicFor<TMessage>()
        {
            throw new System.NotImplementedException();
        }
    }
}
