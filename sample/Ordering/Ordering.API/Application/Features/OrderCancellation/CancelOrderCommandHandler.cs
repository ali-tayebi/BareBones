using System.Threading;
using System.Threading.Tasks;
using BareBones.CQRS.Commands;
using Microsoft.Extensions.Logging;
using Ordering.API.Application.Features.OrderCancellation;
using Ordering.Domain;

namespace Ordering.Application.UseCases.OrderCancellation
{
    public class CancelOrderCommandHandler :
        ICommandHandler<CancelOrderCommand, CancelOrderCommandResult>
    {
        private readonly ILogger<CancelOrderCommandHandler> _logger;
        private readonly IOrderRepository _orderRepository;

        public CancelOrderCommandHandler(
            ILogger<CancelOrderCommandHandler> logger,
            IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<CancelOrderCommandResult> HandleAsync(
            CancelOrderCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("[*]- Executing an order cancellation command...");

            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            return new CancelOrderCommandResult { OrderId = request.OrderId};
        }
    }
}
