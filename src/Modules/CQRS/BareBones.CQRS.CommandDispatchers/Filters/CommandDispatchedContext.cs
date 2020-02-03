namespace BareBones.CQRS.CommandDispatchers.Filters
{
    public class CommandDispatchedContext<TCommand, TResult> where TCommand : class, ICommand
    {
        public TCommand Command { get; }
        public TResult Result { get; }

        public CommandDispatchedContext(TCommand command, TResult result)
        {
            Command = command;
            Result = result;
        }
    }
}
