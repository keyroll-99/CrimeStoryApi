using Microsoft.EntityFrameworkCore;

namespace User.Infrastructure.Data;

public class UserContext: DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        
    }

    public DbSet<Core.Entities.User> Users { get; set; }
}