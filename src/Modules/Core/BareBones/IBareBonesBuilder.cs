using BareBones.StartupTasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones
{
    public interface IBareBonesBuilder
    {
        IServiceCollection Services { get; }
        IBareBonesBuilder RegisterModule<TModule>();
        IConfiguration Configuration { get; }

        IBareBonesBuilder AddStartupTask<TStartupTask>() where TStartupTask : class, IStartupTask;
    }
}
