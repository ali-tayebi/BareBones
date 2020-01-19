using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BareBones.Domain.Aggregates;
using BareBones.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace BareBones.Persistence.EntityFramework
{
    public class RepositoryBase<TEntity, TEntityId> : IRepositoryBase<TEntity, TEntityId>
        where TEntity : EntityBase<TEntityId>, IAggregateRoot
    {
        protected readonly DbContextBase _context;
        protected readonly DbSet<TEntity> DbSet;
        public IUnitOfWork UnitOfWork => _context;


        public IExecutionContext CurrentRequestState { get; }

        public RepositoryBase(DbContextBase dbContext, IExecutionContext currentRequestState)
        {
            _context = dbContext;
            CurrentRequestState = currentRequestState;
            DbSet = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, dynamic>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToArray();
            }

            return query.ToArray();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, dynamic>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToArrayAsync();
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<dynamic>> GetWithSelectorAsync(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, dynamic>> selector,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, dynamic>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).ToArrayAsync();
            }

            return await query.Select(selector).ToListAsync().ConfigureAwait(false);
        }

        public TEntity FirstOrDefault(
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, dynamic>>[] includes)
        {
            var query = DbSet.Where(filter);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault();
        }

        public Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, dynamic>>[] includes)
        {
            var query = DbSet.Where(filter);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefaultAsync();
        }

        public Task<dynamic> FirstOrDefaultWithSelectorAsync(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, dynamic>> selector,
            params Expression<Func<TEntity, dynamic>>[] includes)
        {
            var query = DbSet.Where(filter);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Select(selector).FirstOrDefaultAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Count(filter);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.CountAsync(filter);
        }

        public TEntity GetById(TEntityId id)
        {
            return DbSet.Find(id);
        }

        public async Task<TEntity> GetByIdAsync(TEntityId id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual TEntity Add(TEntity entity)
        {
            var entry = DbSet.Add(entity);
            return entry?.Entity;
        }

        public virtual void AddRange(IEnumerable<TEntity> entity)
        {
            DbSet.AddRange(entity);
        }

        public virtual void MarkAsModified(TEntity entity)
        {
            DbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        // TODO: double check if it is useful
        public virtual void RefreshEntity(TEntity entity)
        {
            _context.Entry(entity).Reload();
        }

        // TODO: double check if it is useful
        public virtual void RefreshAll()
        {
            foreach (var entity in _context.ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> any)
        {
            return DbSet.Any(any);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> any)
        {
            return DbSet.AnyAsync(any);
        }

        public virtual bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        // TODO: double check if it is useful
        public virtual void ResetContextState()
        {
            foreach (var entry in _context.ChangeTracker.Entries().Where(e => e.Entity != null))
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Reload();
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; //Revert changes made to deleted entity.
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}
