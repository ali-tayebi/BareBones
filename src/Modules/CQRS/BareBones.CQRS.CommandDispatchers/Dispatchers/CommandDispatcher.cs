using System;
using System.Threading;
using System.Threading.Tasks;
using BareBones.CQRS.CommandDispatchers.Filters;
using BareBones.CQRS.Commands.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.CQRS.CommandDispatchers.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CommandDispatcher(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var executor = scope.ServiceProvider.GetService<IDispatchFiltersExecutor>();
                var dispatcher = scope.ServiceProvider.GetService<ICommandDispatcher<TCommand, TResult>>();


                TResult result = default;
                try
                {
                    await executor.ExecuteFiltersBeforeDispatchAsync(command);
                    result = await dispatcher.SendAsync(command, cancellationToken);

                    await executor.ExecuteFiltersAfterDispatchAsync(command, result);
                    return result;
                }
                catch (Exception e)
                {
                    var filter = scope.ServiceProvider.GetService<IExceptionCommandDispatchFilter<TCommand, TResult>>();
                    var exceptionContext = new CommandDispatchExceptionContext<TCommand, TResult>(command, result, e);
                    await filter?.OnExceptionAsync(exceptionContext);
                    return exceptionContext.Result;
                }
            }
        }
    }
}
