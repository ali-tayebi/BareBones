using System.Threading.Tasks;
using BareBones.CQRS;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.UseCases.OrderCancellation;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IRequestDispatcher _dispatcher;

        public OrdersController(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("{orderId:int}")]
        public async Task<ActionResult<CancelOrderCommandResult>> Get(int orderId)
        {
            return await _dispatcher.Dispatch(new CancelOrderCommand(orderId));
        }
    }
}
