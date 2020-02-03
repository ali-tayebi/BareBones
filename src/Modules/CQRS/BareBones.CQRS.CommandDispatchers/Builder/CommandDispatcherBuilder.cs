using System;
using System.Reflection;
using BareBones.CQRS.CommandDispatchers.Dispatchers;
using BareBones.CQRS.CommandDispatchers.Filters;
using BareBones.CQRS.Commands.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.CQRS.CommandDispatchers.Builder
{
    public class CommandDispatcherBuilder : ICommandDispatcherBuilder
    {
        private readonly IBareBonesBuilder _builder;

        public CommandDispatcherBuilder(IBareBonesBuilder builder)
        {
            _builder = builder;
        }

        public ICommandDispatcherBuilder AddFilter<TFilter>() where TFilter : class, ICommandDispatchFilter
        {
            _builder.Services.AddScoped<ICommandDispatchFilter, TFilter>();
            return this;
        }

        public ICommandDispatcherBuilder AddFiltersFromAssemblies(params Assembly[] assemblies)
        {
            _builder.Services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo<ICommandDispatchFilter>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return this;
        }

        public ICommandDispatcherBuilder AddAllFilters()
        {
            return AddFiltersFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }

        public ICommandDispatcherBuilder AddDispatcher<TDispatcher>() where TDispatcher : class
        {
            _builder.Services.AddScoped(typeof(ICommandDispatcher<,>), typeof(TDispatcher));
            return this;
        }

        public ICommandDispatcherBuilder AddInProcessDispatcher()
        {
            _builder.Services.AddScoped(
                typeof(ICommandDispatcher<,>),
                typeof(InProcessCommandDispatcher<,>));

            return this;
        }
    }
}
