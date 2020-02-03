using System;

namespace BareBones.CQRS.CommandDispatchers.Filters
{
    public class CommandDispatchExceptionContext<TCommand,TResult>
    {
        public CommandDispatchExceptionContext(TCommand command, TResult result, Exception exception)
        {
            Command = command;
            Result = result;
            Exception = exception;
        }

        public TCommand Command { get; }
        public TResult Result { get; set; }
        public Exception Exception { get; }
    }
}
