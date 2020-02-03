using System;
using System.Threading;
using System.Threading.Tasks;
using BareBones.CQRS.Commands.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.CQRS.CommandGateway
{
    public sealed class CommandGateway : ICommandGateway
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public CommandGateway(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task<TResult> HandleAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand
        {
            using (var scope = _serviceFactory.CreateScope())
            {
                var executor = scope.ServiceProvider.GetService<ICommandGatewayFiltersExecutor>();
                var handler = scope.ServiceProvider.GetService<ICommandHandler<TCommand, TResult>>();

                TResult result = default;
                try
                {
                    await executor.ExecuteFiltersBeforeHandlerAsync(command);
                    result = await handler.HandleAsync(command, cancellationToken);
                    await executor.ExecuteFiltersAfterHandlerAsync(command, result);
                    return result;
                }
                catch (Exception e)
                {
                    var filter = scope.ServiceProvider.GetService<IExceptionCommandGatewayFilter<TCommand, TResult>>();
                    var exceptionContext = new CommandGatewayExceptionContext<TCommand, TResult>(command, result, e);
                    await filter?.OnExceptionAsync(exceptionContext);
                    return exceptionContext.Result;
                }
            }
        }
    }
}
