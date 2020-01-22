using System.Threading;
using System.Threading.Tasks;

namespace BareBones
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
