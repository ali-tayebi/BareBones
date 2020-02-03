using System;

namespace BareBones.Messaging.Outbox
{
    public sealed class InboxMessage : IIdentifiable<string>
    {
        public string IdentityKey { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
