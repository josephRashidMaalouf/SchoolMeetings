using MongoDB.Bson;

namespace SchoolMeetings.Domain.Interfaces;

public interface IMongoRepository<TEntity>
{
    Task<ICollection<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(string id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity, string id);
    Task<bool> DeleteAsync(string id);
}