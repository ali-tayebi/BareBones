using BareBones.CQRS.Commands.Dispatchers.Filters;

namespace BareBones.CQRS.CommandGateway.Builder
{
    public interface ICommandGatewayBuilder
    {
        ICommandGatewayBuilder AddFilter<TFilter>() where TFilter : class, ICommandGatewayFilter;
        ICommandGatewayBuilder AddHandler<TCommand, TResult, THandler>() where THandler : class, ICommandHandler<TCommand, TResult> where TCommand : ICommand;
    }
}
