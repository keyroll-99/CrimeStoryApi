using Authentication.Core.Entities;
using Authentication.Core.Repository;
using Authentication.Infrastructure.Data;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repository;

public class RefreshTokenRepository: Repository<RefreshToken, AuthenticationContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AuthenticationContext appContext) : base(appContext)
    {
    }

    public async Task<bool> TokenExistsAsync(string token)
    {
        return await Entities.AnyAsync(x => x.Token == token);
    }

    public async Task<List<RefreshToken>> GetAllUserRefreshTokensAsync(long userId)
    {
        return await Entities.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<RefreshToken> GetRefreshTokenByTokenAsync(string token)
    {
        return await Entities.SingleAsync(x => x.Token == token);
    }
}