using System.Collections.Generic;

namespace MoneyMaster.Common.Models.Responses;

public class ResponseResult<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; }
    public T Data { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();

    public static ResponseResult<T> CreateSuccess(T data, string message = "Operation successful")
    {
        return new ResponseResult<T>
        {
            Success = true,
            Message = message,
            Data = data,
        };
    }

    public static ResponseResult<T> CreateError(IEnumerable<string> errors, string message)
    {
        return new ResponseResult<T>
        {
            Success = false,
            Message = message,
            Errors = errors,
        };
    }
}
