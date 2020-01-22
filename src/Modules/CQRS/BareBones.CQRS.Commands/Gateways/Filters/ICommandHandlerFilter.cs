using System.Threading.Tasks;

namespace BareBones.CQRS.Commands.Gateways.Filters
{
    public interface ICommandHandlerFilter
    {
        Task OnHandlerExecutingAsync<TCommand>(CommandHandlerExecutingContext<TCommand> context);
        Task OnHandlerExecutedAsync<TCommand, TResult>(CommandHandlerExecutedContext<TCommand, TResult> context);
    }
}
