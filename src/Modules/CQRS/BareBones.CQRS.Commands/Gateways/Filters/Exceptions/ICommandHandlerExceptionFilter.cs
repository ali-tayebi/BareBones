using System.Threading.Tasks;
using BareBones.CQRS.Commands.Gateways.Filters.Exceptions;

namespace BareBones.CQRS.Commands
{
    public interface ICommandHandlerExceptionFilter<TCommand, TResult>
    {
        Task OnExceptionAsync(CommandHandlerExceptionContext<TCommand, TResult> context);
    }
}
