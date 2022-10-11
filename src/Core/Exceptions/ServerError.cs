using Core.Models;

namespace Core.Exceptions;

public class ServerError: Exception
{
    public int StatusCode { get; }
    
    public ServerError(){}
    
    public ServerError(string message): base(message){}

    public ServerError(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public static implicit operator BaseError(ServerError error) => new BaseError(error.Message, error.StatusCode);
}