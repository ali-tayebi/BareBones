using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BareBones;
using BareBones.CQRS.CommandDispatchers.Builder;
using BareBones.CQRS.CommandDispatchers.Filters;
using BareBones.CQRS.Commands.Dispatchers.Filters;
using BareBones.CQRS.CommandGateway.Builder;
using BareBones.Persistence.EntityFramework;
using BareBones.Persistence.EntityFramework.Builder;
using BareBones.Persistence.EntityFramework.Migration;
using BareBones.StartupTasks;
using BareBones.WebApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.API.Application.Features.OrderCancellation;
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
            services.AddControllers(opt =>
            {
                opt.Filters.Add<OperationCancelledExceptionFilter>();
            }).AddNewtonsoftJson();

            services.AddScoped<IOrderRepository, OrderRepositoryBase>();
            services.AddScoped<IExecutionContext, ExecutionContext>();
            services.AddTransient<IExecutionPolicy, SimpleExecutionPolicy>();


            services
                .AddBareBones()
                .AddStartupTask<LoggingStartupTask>()
                .AddQueryGateway()
                .AddCommandDispatcher(dispatcher =>
                {
                    dispatcher
                        .AddFilter<LoggingCommandDispatchFilter>()
                        .AddInProcessDispatcher();
                })
                .AddCommandGateway(gateway =>
                {
                    gateway
                        .AddFilter<LoggingCommandGatewayFilter>()
                        .AddHandler<CancelOrderCommand, CancelOrderCommandResult, CancelOrderCommandHandler>();
                })
                .AddOrderingDbContext(Configuration);
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
        public static IBareBonesBuilder AddOrderingDbContext(this IBareBonesBuilder builder, IConfiguration configuration)
        {
            return builder.AddDbContext<OrderingDbContext>(entityFramework =>
            {
                entityFramework
                    .UseOptions(options =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("OrderingDb"),
                            sqlOptions =>
                            {
                                sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                            });
                    })
                    .AddMigrator()
                    .AddDataSeeder<OrderingDbDataSeeder>()
                    .AddDataSeeder<OrderingDbDataSeeder>(seeder =>
                    {
                        seeder.AddDbDataProvider<IEnumerable<OrderStatus>, PredefinedOrderStatusDataProvider>();

                        if (configuration.GetValue<bool>("DataSeeding:UseCustomizationData"))
                        {
                            seeder.AddDbDataProvider(new JsonLineFileDbDataProvider<CardType>(Path.Combine("Data", "CardTypes.json")));
                        }
                        else
                        {
                            seeder.AddDbDataProvider<IEnumerable<CardType>, PredefinedCardTypeDataProvider>();
                        }
                    });
            });
        }
    }
}
