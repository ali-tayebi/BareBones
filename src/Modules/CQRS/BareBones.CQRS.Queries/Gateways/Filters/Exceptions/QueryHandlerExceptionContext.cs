using System;

namespace BareBones.CQRS.Queries.Gateways.Filters.Exceptions
{
    public class QueryHandlerExceptionContext<TQuery,TResult>
    {
        public QueryHandlerExceptionContext(TQuery query, TResult result, Exception exception)
        {
            Query = query;
            Result = result;
            Exception = exception;
        }

        public TQuery Query { get; }
        public TResult Result { get; }
        public Exception Exception { get; }
    }
}
