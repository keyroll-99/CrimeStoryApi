using Authentication.Contracts.Response;
using Authentication.Contracts.Response.ApiResponse;

namespace Authentication.Contracts;

public interface IAuthenticationService
{
    long? ValidateJwt(string? token);
    string GenerateJwt(long userId);
    Task<RefreshResponse> RefreshToken(string currentToken, string ipAddress, CancellationToken cancellationToken);
    Task<string> GenerateRefreshToken(long userId, string ipAddress);
}