using MongoDB.Bson;

namespace SchoolMeetings.Domain.Interfaces;

public interface IMongoRepository<TEntity>
{
    Task<ICollection<TEntity>> GetAllAsync(string collectionName);
    Task<TEntity?> GetByIdAsync(ObjectId id, string collectionName);
    Task<TEntity> AddAsync(TEntity entity, string collectionName);
    Task<bool> UpdateAsync(TEntity entity, ObjectId id, string collectionName);
    Task<bool> DeleteAsync(ObjectId id, string collectionName);
}