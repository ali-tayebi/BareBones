using MediatR;

namespace BareBones.CQRS
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        string CommandUniqueIdentity { get; }
    }
}
