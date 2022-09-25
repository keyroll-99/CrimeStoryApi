using Core.Entities;

namespace Core.Repositories;

public interface IRepository<T> where T: BaseModel
{
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<List<T>> UpdateRangeAsync(List<T> entities);
    Task DeleteAsync(T entity);
    Task DeleteAsync(long id);
    Task DeleteAsync(Guid hash);
    Task<T> GetByIdAsync(long id);
    Task<T> GetByHashAsync(Guid hash);
}