using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace BareBones.Persistence.Repository.EntityFramework
{
    public interface IDbContextTransactionManager
    {
        IDbContextTransaction CurrentTransaction { get; }
        bool HasActiveTransaction { get; }
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        void RollbackTransaction();
    }
}
