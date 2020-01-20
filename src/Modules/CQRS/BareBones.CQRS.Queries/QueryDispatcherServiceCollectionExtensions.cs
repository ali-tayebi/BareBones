using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.CQRS.Queries
{
    public static class QueryDispatcherServiceCollectionExtensions
    {
        public static IServiceCollection AddQueryDispatcher(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);
            services.AddTransient<IQueryDispatcher, MediatorQueryDispatcher>();
            return services;
        }
    }
}
