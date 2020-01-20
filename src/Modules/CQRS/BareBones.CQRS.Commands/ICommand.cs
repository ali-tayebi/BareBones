using MediatR;

namespace BareBones.CQRS.Commands
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        string CommandUniqueIdentity { get; }
    }
}
