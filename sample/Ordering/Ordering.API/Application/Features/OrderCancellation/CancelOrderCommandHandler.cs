using System.Threading;
using System.Threading.Tasks;
using BareBones.CQRS.Commands;
using Ordering.Domain;

namespace Ordering.Application.UseCases.OrderCancellation
{
    public class CancelOrderCommandHandler :
        ICommandHandler<CancelOrderCommand, CancelOrderCommandResult>
    {
        private readonly IOrderRepository _orderRepository;

        public CancelOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CancelOrderCommandResult> Handle(
            CancelOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            return new CancelOrderCommandResult { OrderId = request.OrderId};
        }
    }
}
