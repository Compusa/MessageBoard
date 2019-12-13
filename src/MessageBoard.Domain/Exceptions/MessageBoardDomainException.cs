using System;

namespace MessageBoard.Domain.Exceptions
{
    public class MessageBoardDomainException : Exception
    {
        public MessageBoardDomainException()
        { }

        public MessageBoardDomainException(string message)
            : base(message)
        { }

        public MessageBoardDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}