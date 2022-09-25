using Authentication.Core.Entities;
using Core.Repositories;

namespace Authentication.Core.Repository;

public interface IRefreshTokenRepository: IRepository<RefreshToken>
{
    Task<bool> TokenExistsAsync(string token);
    Task<List<RefreshToken>> GetAllUserRefreshTokensAsync(long userId);
    Task<RefreshToken> GetRefreshTokenByTokenAsync(string token);
}