using System.Linq.Expressions;

namespace Domain.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    void AddEntity(TEntity entity);
    void RemoveEntity(TEntity entity);
    void UpdateEntity(TEntity entity);
    Task<List<TEntity>> GetAllEntitiesAsync();
    Task<List<TEntity>> FindEntitiesByAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity> FindEntityByAsync(Expression<Func<TEntity, bool>> filter);
}