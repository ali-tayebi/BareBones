using System;
using BareBones.Messaging.Outbox.Outboxes;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.Messaging.Outbox
{
    public class OutboxModule
    {

    }
    public static class Extensions
    {
        private const string SectionName = "outbox";
        private const string RegistryName = "messageBrokers.outbox";

        public static IBareBonesBuilder AddMessageOutbox(this IBareBonesBuilder builder, Action<IMessageOutboxBuilder> outboxConfigurar, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            builder.RegisterModule<OutboxModule>();

            var options = builder.Configuration.GetOptions<OutboxOptions>(sectionName);
            builder.Services.AddSingleton(options);
            var outboxBuilder = new MessageOutboxBuilder(builder, options);

            if (outboxConfigurar is null)
            {
                outboxBuilder.AddInMemory();
            }
            else
            {
                outboxConfigurar(outboxBuilder);
            }

            if (!options.Enabled)
            {
                return builder;
            }

            builder.Services.AddHostedService<OutboxHostedService>();


            return builder;
        }

        public static IMessageOutboxBuilder AddInMemory(this IMessageOutboxBuilder configurator,
            string mongoSectionName = null)
        {
            configurator.Builder.Services.AddTransient<IMessageOutbox, InMemoryMessageOutbox>();

            return configurator;
        }
    }
}
