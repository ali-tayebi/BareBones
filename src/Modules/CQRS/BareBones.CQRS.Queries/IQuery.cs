using MediatR;

namespace BareBones.CQRS.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
