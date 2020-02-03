using System;
using BareBones.CQRS.CommandDispatchers.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.CQRS.CommandDispatchers.Builder
{
    public static class Extensions
    {
        public static IBareBonesBuilder AddCommandDispatcher(this IBareBonesBuilder builder,
            Action<ICommandDispatcherBuilder> dispatcher)
        {
            builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            builder.Services.AddScoped<IDispatchFiltersExecutor, DispatchFiltersExecutor>();

            var commandDispatcherBuilder = new CommandDispatcherBuilder(builder);
            commandDispatcherBuilder.AddInProcessDispatcher();

            dispatcher?.Invoke(commandDispatcherBuilder);

            builder.Services.AddSingleton<ICommandDispatcherBuilder>(commandDispatcherBuilder);
            return builder.RegisterModule<ICommandDispatcher>();
        }
    }
}
