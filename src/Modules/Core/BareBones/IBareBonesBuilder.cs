using Microsoft.Extensions.DependencyInjection;

namespace BareBones
{
    public interface IBareBonesBuilder
    {
        IServiceCollection Services { get; }
        IBareBonesBuilder RegisterModule<TModule>();

        IBareBonesBuilder AddStartupTask<TStartupTask>() where TStartupTask : class, IStartupTask;
    }
}
