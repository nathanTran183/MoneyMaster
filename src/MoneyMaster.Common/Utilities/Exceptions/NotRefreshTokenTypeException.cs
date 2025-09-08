using System;

namespace MoneyMaster.Common.Utilities.Exceptions;

public class NotRefreshTokenTypeException : Exception
{
    public NotRefreshTokenTypeException() { }
    public NotRefreshTokenTypeException(string message) : base(message) { }
    public NotRefreshTokenTypeException(string message, Exception innerException) : base(message, innerException) { }
}
