using System;
using System.Reflection;
using BareBones.CQRS.Commands.Dispatchers.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.CQRS.CommandGateway.Builder
{
    public class CommandGatewayBuilder : ICommandGatewayBuilder
    {
        private readonly IBareBonesBuilder _builder;

        public CommandGatewayBuilder(IBareBonesBuilder builder)
        {
            _builder = builder;
        }
        public ICommandGatewayBuilder AddFilter<TFilter>() where TFilter : class, ICommandGatewayFilter
        {
            _builder.Services.AddScoped<ICommandGatewayFilter, TFilter>();
            return this;
        }

        public ICommandGatewayBuilder AddHandler<TCommand, TResult, THandler>() where THandler : class, ICommandHandler<TCommand, TResult> where TCommand : ICommand
        {
            _builder.Services.AddScoped<ICommandHandler<TCommand, TResult>, THandler >();
            return this;
        }

        public ICommandGatewayBuilder AddFiltersFromAssemblies(params Assembly[] assemblies)
        {
            _builder.Services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return this;
        }

        public ICommandGatewayBuilder AddAllFilters()
        {
            return AddFiltersFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
