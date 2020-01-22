using System.Threading;
using System.Threading.Tasks;

namespace BareBones.CQRS.Queries.Gateways
{
    public interface IQueryGateway
    {
        Task<TResult> SendAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : class, IQuery;
    }
}
