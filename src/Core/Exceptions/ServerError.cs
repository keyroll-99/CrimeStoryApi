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
    
}