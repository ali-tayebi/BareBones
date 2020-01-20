using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.CQRS.Commands
{
    public static class CommandDispatcherServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandDispatcher(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);
            services.AddTransient<ICommandDispatcher, MediatorCommandDispatcher>();
            return services;
        }
    }
}
