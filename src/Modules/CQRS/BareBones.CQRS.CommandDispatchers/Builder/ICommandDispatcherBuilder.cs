using System.Reflection;
using BareBones.CQRS.CommandDispatchers.Filters;

namespace BareBones.CQRS.CommandDispatchers.Builder
{
    public interface ICommandDispatcherBuilder
    {
        ICommandDispatcherBuilder AddFilter<TFilter>() where TFilter : class, ICommandDispatchFilter;
        ICommandDispatcherBuilder AddDispatcher<TDispatcher>() where TDispatcher : class;
        ICommandDispatcherBuilder AddInProcessDispatcher();
        ICommandDispatcherBuilder AddFiltersFromAssemblies(params Assembly[] assemblies);
        ICommandDispatcherBuilder AddAllFilters();
    }
}
