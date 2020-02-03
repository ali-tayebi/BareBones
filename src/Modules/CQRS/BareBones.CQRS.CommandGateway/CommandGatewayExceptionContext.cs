using System;

namespace BareBones.CQRS.CommandGateway
{
    public class CommandGatewayExceptionContext<TCommand,TResult>
    {
        public CommandGatewayExceptionContext(TCommand command, TResult result, Exception exception)
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
