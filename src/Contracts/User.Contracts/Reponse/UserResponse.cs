namespace User.Contracts.Reponse;

public class UserResponse
{
    public long Id { get; set; }
    public Guid Hash { get; set; }
    public string Username { get; set; }
}