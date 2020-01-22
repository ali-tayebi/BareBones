using System;

namespace BareBones
{
    public interface IModuleRegistryRecord<out TModule>
    {
        string Name { get; }
        Func<IServiceProvider, IModuleRegistryRecord<TModule>> ModuleFactory { get; }
    }

    public class ModuleRegistryRecord<TModule> : IModuleRegistryRecord<TModule>
    {
        public string Name { get; }
        public Func<IServiceProvider, IModuleRegistryRecord<TModule>> ModuleFactory { get; }

        public ModuleRegistryRecord(string name)
        {
            Name = name;
            ModuleFactory = services
                => services.GetService(typeof(IModuleRegistryRecord<TModule>)) as IModuleRegistryRecord<TModule>;
        }
    }
}
