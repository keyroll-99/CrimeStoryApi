using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories;

public class Repository<T, AppContextT> : IRepository<T>
    where T : BaseModel
    where AppContextT: DbContext
{
    protected readonly AppContextT _appContext;
    protected readonly DbSet<T> Entities;

    public Repository(AppContextT appContext)
    {
        _appContext = appContext;
        Entities = _appContext.Set<T>();
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.CreateAt = DateTime.UtcNow;
        Entities.Add(entity);
        await _appContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        entity.UpdateAt = DateTime.UtcNow;
        Entities.Update(entity);
        await _appContext.SaveChangesAsync();
        return entity;
    }

    public async Task<List<T>> UpdateRangeAsync(List<T> entities)
    {
        foreach (var entity in entities)
        {
            entity.UpdateAt = DateTime.UtcNow;
        }
        
        Entities.UpdateRange(entities);
        await _appContext.SaveChangesAsync();
        return entities;
    }

    public async Task DeleteAsync(T entity)
    {
        Entities.Remove(entity);
        await _appContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var model = await GetByIdAsync(id);
        await DeleteAsync(model);
    }

    public async Task DeleteAsync(Guid hash)
    {
        var model = await GetByHashAsync(hash);
        await DeleteAsync(model);
    }

    public async Task<T> GetByIdAsync(long id)
    {
        return await Entities.SingleAsync(x => x.Id == id);
    }

    public async Task<T> GetByHashAsync(Guid hash)
    {
        return await Entities.SingleAsync(x => x.Hash == hash);
    }
}