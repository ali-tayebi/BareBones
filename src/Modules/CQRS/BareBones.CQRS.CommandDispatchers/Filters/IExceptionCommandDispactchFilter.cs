using System.Threading.Tasks;

namespace BareBones.CQRS.CommandDispatchers.Filters
{
    public interface IExceptionCommandDispatchFilter<TCommand, TResult> where TCommand : class, ICommand
    {
        Task OnExceptionAsync(CommandDispatchExceptionContext<TCommand, TResult> context);
    }
}
