namespace BareBones.CQRS.Commands.Dispatchers.Filters
{
    public class CommandHandlingContext<TCommand> where TCommand : ICommand
    {
        public TCommand Command { get; }

        public CommandHandlingContext(TCommand command)
        {
            Command = command;
        }
    }
}
