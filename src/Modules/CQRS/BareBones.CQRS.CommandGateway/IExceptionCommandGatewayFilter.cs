using System.Threading.Tasks;

namespace BareBones.CQRS.CommandGateway
{
    public interface IExceptionCommandGatewayFilter<TCommand, TResult> where TCommand : class, ICommand
    {
        Task OnExceptionAsync(CommandGatewayExceptionContext<TCommand, TResult> context);
    }
}
