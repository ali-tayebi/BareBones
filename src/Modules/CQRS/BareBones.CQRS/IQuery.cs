using MediatR;

namespace BareBones.CQRS
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
