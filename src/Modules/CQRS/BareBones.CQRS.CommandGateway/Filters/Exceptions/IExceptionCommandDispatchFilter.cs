using System.Threading.Tasks;

namespace BareBones.CQRS.Commands.Dispatchers.Filters.Exceptions
{
    public interface IExceptionCommandDispatchFilter<TCommand, TResult> where TCommand : class, ICommand
    {
        Task OnExceptionAsync(CommandDispatchExceptionContext<TCommand, TResult> context);
    }
}
