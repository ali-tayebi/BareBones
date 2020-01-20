using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BareBones.CQRS.Commands;
using BareBones.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Application.Features.GettingOrdersById;
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GetOrderByIdQueryResult>> Get(
            [FromRoute] GetOrderByIdQuery query,
            CancellationToken  cancellationToken = default)
        {
            var queryResult = await _queryDispatcher.SendAsync(query, cancellationToken);
            return Ok(queryResult);
        }

        [HttpPut("cancel")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelOrderAsync(
            [FromBody]CancelOrderCommand command,
            CancellationToken  cancellationToken = default)
        {
            var commandResult = await _commandDispatcher.SendAsync(command, cancellationToken);
            return Ok(commandResult);
        }
    }
}
