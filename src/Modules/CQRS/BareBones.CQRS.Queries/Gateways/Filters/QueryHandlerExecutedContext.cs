namespace BareBones.CQRS.Queries.Gateways.Filters
{
    public class QueryHandlerExecutedContext<TQuery, TResult>
    {
        public TQuery Query { get; }
        public TResult Result { get; }

        public QueryHandlerExecutedContext(TQuery query, TResult result)
        {
            Query = query;
            Result = result;
        }
    }
}
