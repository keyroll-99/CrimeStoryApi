using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using User.Core.Repositories;
using User.Infrastructure.Data;

namespace User.Infrastructure.Repository;

public class UserRepository: Repository<Core.Entities.User, UserContext>, IUserRepository
{
    public UserRepository(UserContext appContext) : base(appContext)
    {
    }

    public async Task<List<Core.Entities.User>> GetAll()
    {
        return await Entities.ToListAsync();
    }

    public async Task<bool> UserExists(string username)
    {
        return await Entities.AnyAsync(x => x.Username == username);
    }

    public async Task<Core.Entities.User> GetByUsername(string username)
    {
        return await Entities.SingleAsync(x => x.Username == username);
    }
}