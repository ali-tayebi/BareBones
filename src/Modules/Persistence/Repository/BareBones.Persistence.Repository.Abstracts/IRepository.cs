using BareBones.Domain.Abstracts;

namespace BareBones.Persistence.Repository.Abstracts
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
