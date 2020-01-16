using System.Threading.Tasks;

namespace BareBones.Persistence.Repository.EntityFramework
{
    public interface IBareBonesDbContextEventSubscriber
    {
        Task BeforeSavingChangesAsync(BareBonesDbContext unitOfWorkDbContext);
        Task AfterSavedChangesAsync(BareBonesDbContext unitOfWorkDbContext);
    }
}
