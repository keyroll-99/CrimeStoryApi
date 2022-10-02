namespace Authentication.Contracts.Response.ApiResponse;

public class RefreshResponse
{
    public string RefreshToken { get; set; }
    public string Jwt { get; set; }
}