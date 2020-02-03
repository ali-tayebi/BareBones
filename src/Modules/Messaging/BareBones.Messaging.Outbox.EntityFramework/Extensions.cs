using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.Messaging.Outbox.EntityFramework
{
    public static class Extensions
    {
        public static IMessageOutboxBuilder UseDbContext<TDbContext>(this IMessageOutboxBuilder builder)
            where TDbContext : DbContext
        {

            builder.Builder.Services.AddDbContext<TDbContext>();
            builder.Builder.Services.AddTransient<IMessageOutbox, EntityFrameworkMessageOutbox<TDbContext>>();
            builder.Builder.Services.AddTransient<IMessageOutboxRepository, EntityFrameworkMessageOutbox<TDbContext>>();

            return builder;
        }
    }
}
