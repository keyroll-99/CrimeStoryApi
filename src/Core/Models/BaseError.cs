namespace Core.Models;

public class BaseError
{
    public string Message { get; init; }
    public int Code { get; init; }

    public BaseError()
    {
    }

    public BaseError(string message, int code)
    {
        Message = message;
        Code = code;
    }
}