using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BareBones.CQRS.Commands.Gateways.Filters;
using BareBones.CQRS.Commands.Gateways.Filters.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.CQRS.Commands.Gateways
{
    public sealed class CommandGateway : ICommandGateway
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public CommandGateway(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand
        {
            using (var scope = _serviceFactory.CreateScope())
            {
                var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
                var filters = scope.ServiceProvider.GetServices<ICommandHandlerFilter>().ToArray();

                TResult result = default;
                try
                {
                    await RunBeforeHandlerFilters(command, filters);

                    result = await handler.HandleAsync(command, cancellationToken);

                    await RunAfterHandlerFilters(command, result, filters);

                    return result;
                }
                catch (Exception e)
                {
                    var filter = scope.ServiceProvider.GetService<ICommandHandlerExceptionFilter<TCommand, TResult>>();
                    var exceptionContext = new CommandHandlerExceptionContext<TCommand, TResult>(command, result, e);
                    await filter.OnExceptionAsync(exceptionContext);
                    return exceptionContext.Result;
                }
            }
        }

        private static async Task RunBeforeHandlerFilters<TCommand>(TCommand command, IEnumerable<ICommandHandlerFilter> filters)
        {
            foreach (var filter in filters)
            {
                await filter.OnHandlerExecutingAsync(new CommandHandlerExecutingContext<TCommand>(command));
            }
        }

        private static async Task RunAfterHandlerFilters<TCommand, TResult>(TCommand command, TResult result, IEnumerable<ICommandHandlerFilter> filters)
        {
            foreach (var filter in filters)
            {
                await filter.OnHandlerExecutedAsync(new CommandHandlerExecutedContext<TCommand, TResult>(command, result));
            }
        }


    }
}
