using Microsoft.Extensions.DependencyInjection;
using BareBones.StartupTasks;

namespace BareBones
{
    public static class BareBonesServiceCollectionExtensions
    {
        public static IBareBonesBuilder AddBareBones(this IServiceCollection services)
        {
            return new BareBonesBuilder(services);
        }
    }
}
