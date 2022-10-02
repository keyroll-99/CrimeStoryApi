namespace Core.Models;

public struct LoggedUserData
{
    public long Id { get; set; }
    public string Username { get; set; }
}

public class LoggedUser
{
    private LoggedUserData _loggedUserData;

    public void SetLoggedUser(LoggedUserData loggedUserData)
    {
        _loggedUserData = loggedUserData;
    }

    public LoggedUserData GetLoggedUser()
        => _loggedUserData;
}