using System.Threading;
using System.Threading.Tasks;
using BareBones;
using Microsoft.EntityFrameworkCore;

namespace BareBones.Persistence.EntityFramework.Migration
{
    public class MigrationStartupTask<TDbContext> : IStartupTask
        where TDbContext : DbContext
    {
        protected readonly TDbContext Context;

        public MigrationStartupTask(TDbContext context)
        {
            Context = context;
        }

        Task IStartupTask.ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return MigrationAsync(cancellationToken);
        }

        protected virtual async Task MigrationAsync(CancellationToken cancellationToken)
        {
            await  Context.Database.MigrateAsync(cancellationToken);
        }
    }
}
