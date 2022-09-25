namespace Authentication.Contracts;

public interface IAuthenticationService
{
    long? ValidateJwtToken(string? token);
    string GenerateJwtToken(long userId);
    Task<string> GenerateRefreshToken(long userId, string ipAddress);
    Task RevokeAllRefreshToken(long userId, string ipAddress);
    Task RevokeRefreshToken(string token, string ipAddress);
    Task<bool> IsValidRefreshToken(string token);
}