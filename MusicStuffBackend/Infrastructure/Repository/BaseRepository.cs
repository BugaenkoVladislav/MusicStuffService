using System.Linq.Expressions;
using Domain.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Infrastructure.Repository;

public class BaseRepository<TEntity>(MyDbContext db):IRepository<TEntity> where TEntity : class
{
    private readonly MyDbContext _db = db;

    public virtual void AddEntity(TEntity entity)
    {
        _db.Set<TEntity>().AddAsync(entity);
    }

    public virtual void RemoveEntity(TEntity entity)
    {
        _db.Set<TEntity>().Remove(entity);
    }

    public virtual void UpdateEntity(TEntity entity)
    {
        _db.Set<TEntity>().Update(entity);
    }

    public virtual async Task<List<TEntity>> GetAllEntitiesAsync()
    {
        return await _db.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<List<TEntity>> FindEntitiesByAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _db.Set<TEntity>().Where(filter).ToListAsync();
    }

    public virtual async Task<TEntity> FindEntityByAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _db.Set<TEntity>().FirstAsync(filter);
    }
}