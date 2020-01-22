using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BareBones
{
    public class ModuleRegistry : IModuleRegistry
    {
        private readonly IServiceCollection _services;

        public ModuleRegistry(IServiceCollection services)
        {
            _services = services;
        }

        public void Add<TModule>(IModuleRegistryRecord<TModule> moduleRegistryRecord)
        {
            _services.Replace(new ServiceDescriptor(typeof(IModuleRegistryRecord<TModule>), moduleRegistryRecord));
        }
    }
}
