using System.Threading.Tasks;

namespace BareBones.CQRS.Commands.Dispatchers.Filters
{
    public interface ICommandGatewayFilter
    {
        Task OnHandlingAsync<TCommand>(CommandHandlingContext<TCommand> context) where TCommand : class, ICommand;
        Task OnHandledAsync<TCommand, TResult>(CommandHandledContext<TCommand, TResult> context)  where TCommand : class, ICommand;
    }
}
