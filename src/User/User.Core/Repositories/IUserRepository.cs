using Core.Repositories;

namespace User.Core.Repositories;

public interface IUserRepository: IRepository<Entities.User>
{
    Task<List<Entities.User>> GetAll();
}
