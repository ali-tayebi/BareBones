using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using BareBones.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BareBones.Persistence.EntityFramework
{
    public abstract class DbContextBase : DbContext, IUnitOfWork, IHandleTransaction
    {
        public IDbContextTransaction CurrentTransaction { get; protected set; }
        public bool HasActiveTransaction => CurrentTransaction != null;
        public IEnumerable<IDbContextInterceptor> Interceptors { get; }

        public DbContextBase(DbContextOptions options) : base(options) {}

        public DbContextBase(DbContextOptions options, IEnumerable<IDbContextInterceptor> interceptors) : base(options)
        {
            Interceptors = interceptors;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var interceptor in Interceptors)
            {
                await interceptor.BeforeSaveChangesAsync(this);
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var subscriber in Interceptors)
            {
                await subscriber.AfterSaveChangesAsync(this);
            }

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (CurrentTransaction != null) return null;

            CurrentTransaction = await Database.BeginTransactionAsync(cancellationToken);

            return CurrentTransaction;
        }


        // TODO: This may need to be changed as an extension method. It requires Microsoft.EntityFrameworkCore.Relational

        public async Task<IDbContextTransaction> BeginTransactionAsync(
            IsolationLevel isolationLevel,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (CurrentTransaction != null) return null;

            CurrentTransaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken);

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
