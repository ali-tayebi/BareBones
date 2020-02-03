using System;
using BareBones.CQRS.Commands.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.CQRS.CommandGateway.Builder
{
    public static class Extensions
    {
        public static IBareBonesBuilder AddCommandGateway(this IBareBonesBuilder builder,
            Action<ICommandGatewayBuilder> gateway)
        {

            builder.Services.AddSingleton<ICommandGateway, CommandGateway>();
            builder.Services.AddScoped<ICommandGatewayFiltersExecutor, CommandGatewayFiltersExecutor>();

            var gatewayBuilder = new CommandGatewayBuilder(builder);

            gateway?.Invoke(gatewayBuilder);

            builder.Services.AddSingleton<ICommandGatewayBuilder>(gatewayBuilder);
            return builder.RegisterModule<ICommandGateway>();
        }
    }
}
