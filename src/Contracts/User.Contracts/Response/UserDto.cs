namespace User.Contracts.Response;

public class UserDto
{
    public long Id { get; set; }
    public Guid Hash { get; set; }
    public string Username { get; set; }
}