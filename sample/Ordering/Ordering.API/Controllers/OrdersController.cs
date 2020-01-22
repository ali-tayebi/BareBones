using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BareBones.CQRS.Commands;
using BareBones.CQRS.Commands.Gateways;
using BareBones.CQRS.Queries;
using BareBones.CQRS.Queries.Gateways;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Application.Features.GettingOrdersById;
using Ordering.API.Application.Features.OrderCancellation;
using Ordering.Application.UseCases.OrderCancellation;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IQueryGateway _queryGateway;
        private readonly ICommandGateway _commandGateway;

        public OrdersController(IQueryGateway queryGateway, ICommandGateway commandDispatcher)
        {
            _queryGateway = queryGateway;
            _commandGateway = commandDispatcher;
        }

        [HttpGet("{orderId:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GetOrderByIdQueryResult>> Get(
            [FromRoute] GetOrderByIdQuery query,
            CancellationToken  cancellationToken = default)
        {
            var queryResult = await _queryGateway.SendAsync<GetOrderByIdQuery, GetOrderByIdQueryResult>(query, cancellationToken);
            return Ok(queryResult);
        }

        [HttpPut("cancel")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelOrderAsync(
            [FromBody]CancelOrderCommand command,
            CancellationToken  cancellationToken = default)
        {
            var commandResult = await _commandGateway.SendAsync<CancelOrderCommand, CancelOrderCommandResult>(command, cancellationToken);
            return Ok(commandResult);
        }
    }
}
