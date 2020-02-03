using System;
using System.Threading;
using System.Threading.Tasks;
using BareBones.CQRS.Commands.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.CQRS.CommandDispatchers.Dispatchers
{
    public class InProcessCommandDispatcher<TCommand, TResult> : ICommandDispatcher<TCommand, TResult> where TCommand : class, ICommand
    {
        private readonly IServiceProvider _serviceProvider;

        public InProcessCommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<TResult> SendAsync(TCommand command, CancellationToken cancellationToken = default)
        {
            var gateway = _serviceProvider.GetRequiredService<ICommandGateway>();
            return await gateway.HandleAsync<TCommand, TResult>(command, cancellationToken);
        }
    }
}
