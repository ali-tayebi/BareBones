using System.Threading;
using System.Threading.Tasks;
using BareBones.CQRS;
using BareBones.CQRS.Commands.Dispatchers;

namespace BareBones.Messaging.CQRS
{
    public class MessageBusCommandDispatcher<TCommand, TResult> : ICommandDispatcher<TCommand, TResult> where TCommand : class, ICommand
    {
        private readonly IBusClient _busClient;

        public MessageBusCommandDispatcher(IBusClient busClient)
        {
            _busClient = busClient;
        }
        public Task<TResult> SendAsync(TCommand command, CancellationToken cancellationToken = default)
            => _busClient.SendAsync<TCommand, string, TResult>(command.IdentityKey, command);
    }
}
