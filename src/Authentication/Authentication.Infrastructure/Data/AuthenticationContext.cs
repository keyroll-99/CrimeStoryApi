using Authentication.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Data;

public class AuthenticationContext: DbContext
{
    public AuthenticationContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
}