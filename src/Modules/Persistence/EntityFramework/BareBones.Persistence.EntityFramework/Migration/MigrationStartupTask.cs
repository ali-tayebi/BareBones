using System.Threading;
using System.Threading.Tasks;
using BareBones.Core;
using Microsoft.EntityFrameworkCore;

namespace BareBones.Persistence.EntityFramework.Migration
{
    public class MigrationStartupTask<TDbContext> : IStartupTask
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        public MigrationStartupTask(TDbContext context)
        {
            _context = context;
        }
        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            await  _context.Database.MigrateAsync(cancellationToken);
        }
    }
}
