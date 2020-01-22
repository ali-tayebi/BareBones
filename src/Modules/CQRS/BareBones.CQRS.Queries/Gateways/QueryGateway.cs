using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BareBones.CQRS.Queries.Gateways.Filters;
using BareBones.CQRS.Queries.Gateways.Filters.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.CQRS.Queries.Gateways
{
    public sealed class QueryGateway : IQueryGateway
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public QueryGateway(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : class, IQuery
        {
            using(var scope = _serviceFactory.CreateScope())
            {
                var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
                var filters = scope.ServiceProvider.GetServices<IQueryHandlerFilter>().ToArray();

                TResult result = default;
                try
                {
                    await RunBeforeHandlerFilters(query, filters);

                    result = await handler.HandleAsync(query, cancellationToken);

                    await RunAfterHandlerFilters(query, result, filters);

                    return result;
                }
                catch (Exception e)
                {
                    var filter = scope.ServiceProvider.GetService<IQueryHandlerExceptionFilter<TQuery, TResult>>();
                    var exceptionContext = new QueryHandlerExceptionContext<TQuery, TResult>(query, result, e);
                    await filter.OnExceptionAsync(exceptionContext);
                    return exceptionContext.Result;
                }
            }
        }

        private static async Task RunBeforeHandlerFilters<TQuery>(TQuery query, IEnumerable<IQueryHandlerFilter> filters)
        {
            foreach (var filter in filters)
            {
                await filter.OnHandlerExecutingAsync(new QueryHandlerExecutingContext<TQuery>(query));
            }
        }

        private static async Task RunAfterHandlerFilters<TQuery, TResult>(TQuery command, TResult result, IEnumerable<IQueryHandlerFilter> filters)
        {
            foreach (var filter in filters)
            {
                await filter.OnHandlerExecutedAsync(new QueryHandlerExecutedContext<TQuery, TResult>(command, result));
            }
        }


    }
}
