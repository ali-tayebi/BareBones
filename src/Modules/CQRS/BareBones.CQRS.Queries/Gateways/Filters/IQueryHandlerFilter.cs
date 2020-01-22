using System.Threading.Tasks;

namespace BareBones.CQRS.Queries.Gateways.Filters
{
    public interface IQueryHandlerFilter
    {
        Task OnHandlerExecutingAsync<TQuery>(QueryHandlerExecutingContext<TQuery> context);
        Task OnHandlerExecutedAsync<TQuery, TResult>(QueryHandlerExecutedContext<TQuery, TResult> context);
    }
}
