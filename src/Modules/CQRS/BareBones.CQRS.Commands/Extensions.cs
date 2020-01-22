using System;
using BareBones.CQRS.Commands;
using BareBones.CQRS.Commands.Gateways;
using BareBones.CQRS.Commands.Gateways.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones
{
    public static class Extensions
    {
        public static IBareBonesBuilder AddCommandGateway(this IBareBonesBuilder builder)
        {
            builder.Services.AddSingleton<ICommandGateway, CommandGateway>();

            builder.Services.Scan(selector =>
                selector.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandlerFilter)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder.RegisterModule<ICommandGateway>();
        }
    }
}
