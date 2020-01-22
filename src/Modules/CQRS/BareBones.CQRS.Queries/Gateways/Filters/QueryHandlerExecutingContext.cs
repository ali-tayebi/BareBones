namespace BareBones.CQRS.Queries.Gateways.Filters
{
    public class QueryHandlerExecutingContext<TQuery>
    {
        public TQuery Query { get; }

        public QueryHandlerExecutingContext(TQuery query)
        {
            Query = query;
        }
    }
}
