namespace BareBones.CQRS.Commands.Gateways.Filters
{
    public class CommandHandlerExecutingContext<TCommand>
    {
        public TCommand Command { get; }

        public CommandHandlerExecutingContext(TCommand command)
        {
            Command = command;
        }
    }
}
