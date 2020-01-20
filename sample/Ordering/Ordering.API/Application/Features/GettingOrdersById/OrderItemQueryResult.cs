namespace Ordering.API.Application.Features.GettingOrdersById
{
    public class OrderItemQueryResult
    {
        public string ProductName { get; set; }
        public int Units { get; set; }
        public double UnitPrice { get; set; }
        public string PictureUrl { get; set; }
    }
}
