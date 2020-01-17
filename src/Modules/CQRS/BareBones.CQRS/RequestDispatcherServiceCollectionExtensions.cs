using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.CQRS
{
    public static class RequestDispatcherServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatorRequestDispatcher(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);
            services.AddTransient<IRequestDispatcher, MediatorRequestDispatcher>();
            return services;
        }
    }
}
