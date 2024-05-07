using MongoDB.Bson;

namespace SchoolMeetings.Domain.Interfaces;

public interface IMongoRepository<TEntity>
{
    Task<ICollection<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(ObjectId id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity, ObjectId id);
    Task<bool> DeleteAsync(ObjectId id);
}