namespace Core.Models;

public struct LoggedUserData
{
    public long Id { get; set; }
    public string Username { get; set; }
}

public class LoggedUser
{
    public LoggedUserData LoggedUserData { get; private set; }

    public void SetLoggedUser(LoggedUserData loggedUserData)
    {
        if (LoggedUserData.Id != 0)
        {
            return;
        }
        LoggedUserData = loggedUserData;
    }

    public LoggedUserData GetLoggedUser()
        => LoggedUserData;
}