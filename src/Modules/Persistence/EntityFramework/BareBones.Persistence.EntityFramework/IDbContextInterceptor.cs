using System.Threading.Tasks;

namespace BareBones.Persistence.EntityFramework
{
    public interface IDbContextInterceptor
    {
        Task BeforeSaveChangesAsync(DbContextBase dbContextBase);
        Task AfterSaveChangesAsync(DbContextBase dbContextBase);
    }
}
