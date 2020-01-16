using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BareBones.Persistence.Repository.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BareBones.Persistence.Repository.EntityFramework
{
    public abstract class BareBonesDbContext : DbContext, IUnitOfWork, IDbContextTransactionManager
    {
        public IDbContextTransaction CurrentTransaction { get; protected set; }
        public bool HasActiveTransaction => CurrentTransaction != null;
        public IEnumerable<IBareBonesDbContextEventSubscriber> EventSubscribers { get; }

        public BareBonesDbContext(DbContextOptions options) : base(options) {}

        public BareBonesDbContext(DbContextOptions options, IEnumerable<IBareBonesDbContextEventSubscriber> eventSubscribers) : base(options)
        {
            EventSubscribers = eventSubscribers;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var subscriber in EventSubscribers)
            {
                await subscriber.BeforeSavingChangesAsync(this);
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var subscriber in EventSubscribers)
            {
                await subscriber.AfterSavedChangesAsync(this);
            }

            return true;
        }

        // TODO: This may need to be changed as an extension method. It requires Microsoft.EntityFrameworkCore.Relational
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (CurrentTransaction != null) return null;

            CurrentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            return CurrentTransaction;
        }
        
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != CurrentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (CurrentTransaction != null)
                {
                    CurrentTransaction.Dispose();
                    CurrentTransaction = null;
                }
            }
        }
        
        public void RollbackTransaction()
        {
            try
            {
                CurrentTransaction?.Rollback();
            }
            finally
            {
                if (CurrentTransaction != null)
                {
                    CurrentTransaction.Dispose();
                    CurrentTransaction = null;
                }
            }
        }
    }
}
