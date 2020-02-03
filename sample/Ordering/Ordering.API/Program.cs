using System;
using System.Reflection;
using BareBones;
using BareBones.CQRS;
using BareBones.Persistence.EntityFramework.Builder;
using BareBones.StartupTasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Ordering.API.Application.Features.GettingOrdersById;
using Ordering.Application.UseCases.OrderCancellation;
using Ordering.Persistence;
using Ordering.Persistence.DataSeeding;

namespace Ordering.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
                .CreateDefaultBuilder(args)
                // .UseBareBonesBuilder(builder =>
                // {
                //     builder
                //         .AddStartupTask<LoggingStartupTask>()
                //         .AddQueryGateway(typeof(GetOrderByIdQuery).Assembly)
                //         .AddCommandGateway(typeof(CancelOrderCommand).Assembly, typeof(LoggingCommandDispatchFilter).Assembly)
                //         .UseDbContext<OrderingDbContext>(entityFramework =>
                //         {
                //             entityFramework
                //                 .UseOptions(options =>
                //                  {
                //                      options.UseSqlServer(
                //                          configuration.GetConnectionString("OrderingDb"),
                //                          sqlOptions =>
                //                          {
                //                              sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                //                              sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                //                          });
                //                  })
                //                 .AddMigrator()
                //                 .AddDataSeeder<OrderingDbDataSeeder>();
                //         });
                // })
                .ConfigureLogging(logging => logging
                    .ClearProviders()
                    .AddConsole(opt=>
                {
                    opt.IncludeScopes = true;
                    opt.DisableColors = false;
                    opt.Format = ConsoleLoggerFormat.Default;
                }))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>();
                });
    }

    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseBareBonesBuilder(this IHostBuilder hostBuilder, Action<IBareBonesBuilder> action)
        {
            hostBuilder.ConfigureServices(services =>
            {
                var bareBonesBuilder = services.AddBareBones();
                action?.Invoke(bareBonesBuilder);
            });
            return hostBuilder;
        }
    }
}
