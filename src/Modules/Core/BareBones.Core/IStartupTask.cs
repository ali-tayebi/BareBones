using System.Threading;
using System.Threading.Tasks;

namespace BareBones.Core
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
