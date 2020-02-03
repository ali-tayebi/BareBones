namespace BareBones.CQRS.Commands.Dispatchers.Filters
{
    public class CommandHandledContext<TCommand, TResult> where TCommand : class, ICommand
    {
        public TCommand Command { get; }
        public TResult Result { get; }

        public CommandHandledContext(TCommand command, TResult result)
        {
            Command = command;
            Result = result;
        }
    }
}
