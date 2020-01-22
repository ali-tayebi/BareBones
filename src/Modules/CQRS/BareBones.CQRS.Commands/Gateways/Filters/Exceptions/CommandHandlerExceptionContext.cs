using System;

namespace BareBones.CQRS.Commands.Gateways.Filters.Exceptions
{
    public class CommandHandlerExceptionContext<TCommand,TResult>
    {
        public CommandHandlerExceptionContext(TCommand command, TResult result, Exception exception)
        {
            Command = command;
            Result = result;
            Exception = exception;
        }

        public TCommand Command { get; }
        public TResult Result { get; }
        public Exception Exception { get; }
    }
}
