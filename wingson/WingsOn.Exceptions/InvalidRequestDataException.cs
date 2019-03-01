using System;

namespace WingsOn.Exceptions
{
    public class InvalidRequestDataException: Exception
    {
        public InvalidRequestDataException()
        {
        }

        public InvalidRequestDataException(string message)
            : base(message)
        {
        }

        public InvalidRequestDataException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
