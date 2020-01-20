using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BareBones.CQRS;
using BareBones.Persistence.EntityFramework;
using BareBones.Persistence.EntityFramework.Migration;
using BareBones.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.API.Infrastructure.DB;
using Ordering.Application.UseCases.OrderCancellation;
using Ordering.Domain;
using Ordering.Domain.Models.BuyerAggregate;
using Ordering.Persistence;
using Ordering.Persistence.DataSeeding;
using Ordering.Persistence.Repositories;

namespace Ordering.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddMediatorRequestDispatcher(typeof(CancelOrderCommandHandler).Assembly);

            services.AddScoped<IOrderRepository, OrderRepositoryBase>();
            services.AddScoped<IExecutionContext, ExecutionContext>();
            services.AddOrderingDbContext<OrderingDbContext>(Configuration);

            services.AddHostedService<StartupTaskHostedService>();
            services.AddStartupTask<MigrationStartupTask<OrderingDbContext>>();
            services.AddStartupTask<LoggingStartupTask>();
            services.AddStartupTask<DataSeedingStartupTask<OrderingDbContext>>();
            services.AddTransient<IDbDataSeeder<OrderingDbContext>, OrderingDbDataSeeder>();

            services.AddSingleton<
               IDbDataProvider<IEnumerable<OrderStatus>>,
               PredefinedOrderStatusDataProvider>();

            if (Configuration.GetValue<bool>("DataSeeding:UseCustomizationData"))
            {
                services.AddSingleton<
                    IDbDataProvider<IEnumerable<CardType>>>(
                    new JsonLineFileDbDataProvider<CardType>(Path.Combine("Data", "CardTypes.json")));
            }
            else
            {
                services.AddSingleton<
                    IDbDataProvider<IEnumerable<CardType>>,
                    PredefinedCardTypeDataProvider>();
            }

            services.AddTransient<IExecutionPolicy, SimpleExecutionPolicy>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrderingDbContext<T>(this IServiceCollection services, IConfiguration configuration)
            where T : DbContext
        {
            services
                .AddDbContext<OrderingDbContext>(options =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("OrderingDb"),
                            sqlServerOptionsAction: sqlOptions =>
                            {
                                sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            });
                    },
                    ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                );

            return services;
        }
    }
}
