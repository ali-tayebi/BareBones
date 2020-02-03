using System.Threading.Tasks;

namespace BareBones.CQRS.Commands.Dispatchers
{
    public interface ICommandGatewayFiltersExecutor
    {
        Task ExecuteFiltersBeforeHandlerAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
        Task ExecuteFiltersAfterHandlerAsync<TCommand, TResult>(TCommand command, TResult result) where TCommand : class, ICommand;
    }
}
