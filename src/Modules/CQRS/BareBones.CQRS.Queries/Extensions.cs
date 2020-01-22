using System;
using BareBones.CQRS.Queries;
using BareBones.CQRS.Queries.Gateways;
using BareBones.CQRS.Queries.Gateways.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones
{
    public static class Extensions
    {
        public static IBareBonesBuilder AddQueryGateway(this IBareBonesBuilder builder)
        {
            builder.Services.AddSingleton<IQueryGateway, QueryGateway>();

            builder.Services.Scan(selector =>
                selector.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandlerFilter)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder.RegisterModule<IQueryGateway>();
        }
    }
}
