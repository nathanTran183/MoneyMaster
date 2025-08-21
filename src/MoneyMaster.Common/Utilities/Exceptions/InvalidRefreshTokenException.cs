using System;

namespace MoneyMaster.Common.Utilities.Exceptions
{
    public class InvalidRefreshTokenException : Exception
    {
        public InvalidRefreshTokenException() { }
        public InvalidRefreshTokenException(string message) : base(message) { }
        public InvalidRefreshTokenException(string message, Exception innerException) : base(message, innerException) { }
    }
}
