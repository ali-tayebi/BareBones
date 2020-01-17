using System.Threading.Tasks;

namespace BareBones.CQRS
{
    public interface IRequestDispatcher
    {
        Task<TResult> Dispatch<TResult>(ICommand<TResult> command);
        Task<TResult> Dispatch<TResult>(IQuery<TResult> query);
    }
}
