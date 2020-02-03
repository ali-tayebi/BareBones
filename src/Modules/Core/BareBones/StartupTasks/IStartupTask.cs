using System.Threading;
using System.Threading.Tasks;

namespace BareBones.StartupTasks
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
