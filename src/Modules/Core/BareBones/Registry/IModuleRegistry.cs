namespace BareBones
{
    public interface IModuleRegistry
    {
        void Add<TModule>(IModuleRegistryRecord<TModule> moduleRegistryRecord);
    }
}
