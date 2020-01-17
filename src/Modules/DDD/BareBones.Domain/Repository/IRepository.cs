using BareBones.Domain.Aggregates;

namespace BareBones.Domain.Repository
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
