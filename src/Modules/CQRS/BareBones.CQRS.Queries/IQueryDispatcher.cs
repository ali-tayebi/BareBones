using System.Threading.Tasks;

namespace BareBones.CQRS.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> SendAsync<TResult>(IQuery<TResult> query);
    }
}
