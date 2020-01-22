using System;
using BareBones.Persistence.EntityFramework.Migration;
using BareBones.StartupTasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.Persistence.EntityFramework.Builder
{
    public static class Extensions
    {
        public static IBareBonesBuilder AddEntityFramework<TDbContext>(this IBareBonesBuilder builder,
            Action<BareBonesEntityFrameworkBuilder<TDbContext>> action) where TDbContext : DbContext
        {
            var efBuilder = new BareBonesEntityFrameworkBuilder<TDbContext>(builder);
            action?.Invoke(efBuilder);
            return builder;
        }

        public static IBareBonesBuilder AddDbContext<TDbContext>(this IBareBonesBuilder builder,
            Action<BareBonesEntityFrameworkBuilder<TDbContext>> action) where TDbContext : DbContext
        {
            var efBuilder = new BareBonesEntityFrameworkBuilder<TDbContext>(builder);
            action?.Invoke(efBuilder);
            return builder;
        }
    }

    public class BareBonesEntityFrameworkBuilder<TDbContext> where TDbContext : DbContext
    {
        private readonly IBareBonesBuilder _builder;

        public BareBonesEntityFrameworkBuilder(IBareBonesBuilder builder)
        {
            _builder = builder;
        }
        public BareBonesEntityFrameworkBuilder<TDbContext> UseOptions(Action<DbContextOptionsBuilder> options)
        {
            _builder.Services.AddDbContext<TDbContext>(options);
            return this;
        }

        public BareBonesEntityFrameworkBuilder<TDbContext> AddMigrator()
        {
            _builder.AddStartupTask<MigrationStartupTask<TDbContext>>();
            return this;
        }

        public BareBonesEntityFrameworkBuilder<TDbContext> AddMigrationStartTask<TMigrationTask>() where TMigrationTask : class, IStartupTask
        {
            _builder.AddStartupTask<TMigrationTask>();
            return this;
        }

        public BareBonesEntityFrameworkBuilder<TDbContext> AddDataSeeder<TDataSeeder>() where TDataSeeder : class, IDbDataSeeder<TDbContext>
        {
            _builder.Services.AddStartupTask<DataSeedingStartupTask<TDbContext>>();
            _builder.Services.AddTransient<IDbDataSeeder<TDbContext>, TDataSeeder>();

            return this;
        }

        public BareBonesEntityFrameworkBuilder<TDbContext> AddDataSeeder<TDataSeeder>(Action<DataSeederBuilder<TDbContext>> action) where TDataSeeder : class, IDbDataSeeder<TDbContext>
        {
            var dataSeederBuilder = new DataSeederBuilder<TDbContext>(_builder);
            action?.Invoke(dataSeederBuilder);

            return this;
        }

    }

    public class DataSeederBuilder<TDbContext> where TDbContext : DbContext
    {
        private readonly IBareBonesBuilder _builder;

        public DataSeederBuilder(IBareBonesBuilder builder)
        {
            _builder = builder;
        }
        public DataSeederBuilder<TDbContext> AddDbDataProvider<TData, TDbDataProvider>() where TDbDataProvider : class, IDbDataProvider<TData>
        {
            _builder.Services.AddSingleton<IDbDataProvider<TData>, TDbDataProvider>();

            return this;
        }

        public DataSeederBuilder<TDbContext> AddDbDataProvider<TData>(IDbDataProvider<TData> dataProvider)
        {
            _builder.Services.AddSingleton(dataProvider);

            return this;
        }
    }
}
