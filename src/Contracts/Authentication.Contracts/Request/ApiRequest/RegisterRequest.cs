namespace Authentication.Contracts.Request.ApiRequest;

public class RegisterRequest
{
    public string Username { get; init; }
    public string Password { get; init; }
    public string Email { get; set; }
}