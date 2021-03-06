using BareBones;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.StartupTasks
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStartupTask<T>(this IServiceCollection services)
            where T : class, IStartupTask
            => services.AddTransient<IStartupTask, T>();
    }
}
