namespace BareBones.CQRS.Commands.Gateways.Filters
{
    public class CommandHandlerExecutedContext<TCommand, TResult>
    {
        public TCommand Command { get; }
        public TResult Result { get; }

        public CommandHandlerExecutedContext(TCommand command, TResult result)
        {
            Command = command;
            Result = result;
        }
    }
}
