using System.Threading.Tasks;
using BareBones.CQRS.Commands;
using BareBones.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.UseCases.OrderCancellation;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public OrdersController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet("{orderId:int}")]
        public async Task<ActionResult<CancelOrderCommandResult>> Get(int orderId)
        {
            return await _commandDispatcher.SendAsync(new CancelOrderCommand(orderId));
        }
    }
}
