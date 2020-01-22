using System.Threading.Tasks;

namespace BareBones.CQRS.Queries.Gateways.Filters.Exceptions
{
    public interface IQueryHandlerExceptionFilter<TCommand, TResult>
    {
        Task OnExceptionAsync(QueryHandlerExceptionContext<TCommand, TResult> context);
    }
}
