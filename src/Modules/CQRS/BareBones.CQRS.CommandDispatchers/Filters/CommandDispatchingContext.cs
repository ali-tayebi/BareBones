using BareBones.CQRS;

namespace BareBones.CQRS.CommandDispatchers.Filters
{
    public class CommandDispatchingContext<TCommand> where TCommand : ICommand
    {
        public TCommand Command { get; }

        public CommandDispatchingContext(TCommand command)
        {
            Command = command;
        }
    }
}
