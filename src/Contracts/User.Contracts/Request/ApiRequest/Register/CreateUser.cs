namespace User.Contracts.Request.ApiRequest.Register;

public class CreateUser
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}