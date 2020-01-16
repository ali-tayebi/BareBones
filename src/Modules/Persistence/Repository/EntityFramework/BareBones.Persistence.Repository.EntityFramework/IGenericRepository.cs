using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BareBones.Domain.Abstracts;
using BareBones.Persistence.Repository.Abstracts;

namespace BareBones.Persistence.Repository.EntityFramework
{
    public interface IGenericRepository<TAggregateRoot, in TIdentity> : IRepository<TAggregateRoot>
        where TAggregateRoot : EntityBase<TIdentity>, IAggregateRoot
    {
        IExecutionContext CurrentRequestState { get; }

        IEnumerable<TAggregateRoot> Get(
            Expression<Func<TAggregateRoot, bool>> filter = null,
            Func<IQueryable<TAggregateRoot>, IOrderedQueryable<TAggregateRoot>> orderBy = null,
            params Expression<Func<TAggregateRoot, dynamic>>[] includes);

        Task<IEnumerable<TAggregateRoot>> GetAsync(
            Expression<Func<TAggregateRoot, bool>> filter = null,
            Func<IQueryable<TAggregateRoot>, IOrderedQueryable<TAggregateRoot>> orderBy = null,
            params Expression<Func<TAggregateRoot, dynamic>>[] includes);

        Task<IEnumerable<dynamic>> GetWithSelectorAsync(
            Expression<Func<TAggregateRoot, bool>> filter,
            Expression<Func<TAggregateRoot, dynamic>> selector,
            Func<IQueryable<TAggregateRoot>, IOrderedQueryable<TAggregateRoot>> orderBy = null,
            params Expression<Func<TAggregateRoot, dynamic>>[] includes);

        TAggregateRoot FirstOrDefault(
            Expression<Func<TAggregateRoot, bool>> filter = null,
            params Expression<Func<TAggregateRoot, dynamic>>[] includes);

        Task<TAggregateRoot> FirstOrDefaultAsync(
            Expression<Func<TAggregateRoot, bool>> filter = null,
            params Expression<Func<TAggregateRoot, dynamic>>[] includes);

        Task<dynamic> FirstOrDefaultWithSelectorAsync(
            Expression<Func<TAggregateRoot, bool>> filter,
            Expression<Func<TAggregateRoot, dynamic>> selector,
            params Expression<Func<TAggregateRoot, dynamic>>[] includes);

        int Count(Expression<Func<TAggregateRoot, bool>> filter);
        Task<int> CountAsync(Expression<Func<TAggregateRoot, bool>> filter);

        TAggregateRoot GetById(TIdentity id);
        Task<TAggregateRoot> GetByIdAsync(TIdentity id);

        TAggregateRoot Add(TAggregateRoot entity);
        void AddRange(IEnumerable<TAggregateRoot> entity);
        void MarkAsModified(TAggregateRoot entity);

        bool Any(Expression<Func<TAggregateRoot, bool>> any);
        Task<bool> AnyAsync(Expression<Func<TAggregateRoot, bool>> any);

        Task<int> SaveChangesAsync();

        void ResetContextState();
        bool HasChanges();
        void RefreshEntity(TAggregateRoot entity);
        void RefreshAll();
    }
}
