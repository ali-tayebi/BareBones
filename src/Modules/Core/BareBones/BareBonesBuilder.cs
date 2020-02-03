using BareBones.StartupTasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones
{
    public class BareBonesBuilder : IBareBonesBuilder
    {
        public IServiceCollection Services { get; }
        public IModuleRegistry Registry { get; }
        public IConfiguration Configuration => Services.BuildServiceProvider().GetService<IConfiguration>();

        public BareBonesBuilder(IServiceCollection services)
        {
            Services = services;
            Services.AddHostedService<StartupTaskHostedService>();

            Registry = new ModuleRegistry(services);
        }

        public IBareBonesBuilder RegisterModule<TModule>()
        {
            Registry.Add(new ModuleRegistryRecord<TModule>(typeof(TModule).FullName));
            return this;
        }



        public IBareBonesBuilder AddStartupTask<TStartupTask>() where TStartupTask : class, IStartupTask
        {
            Services.AddTransient<IStartupTask, TStartupTask>();
            return this;
        }
    }
}
