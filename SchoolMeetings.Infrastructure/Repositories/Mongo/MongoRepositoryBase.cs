using MongoDB.Bson;
using MongoDB.Driver;
using SchoolMeetings.Domain.Interfaces;


namespace SchoolMeetings.Infrastructure.Repositories.Mongo;

public class MongoRepositoryBase<TEntity>(string collectionName) : IMongoRepository<TEntity>
{
    //TODO: replace connectionstring with when hosting the db live
    protected const string ConnectionsString = "mongodb://localhost:27017/";
    protected const string DataBaseName = "SchoolMeetingsMongoDb";

    protected readonly string _collectionName = collectionName;


    protected IMongoCollection<T> ConnectToMongo<T>()
    {
        var client = new MongoClient(ConnectionsString);
        var db = client.GetDatabase(DataBaseName);
        return db.GetCollection<T>(_collectionName);
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        var collection = ConnectToMongo<TEntity>();

        var filter = Builders<TEntity>.Filter.Empty;

        var results = await collection.FindAsync(filter);

        return await results.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(ObjectId id)
    {
        var collection = ConnectToMongo<TEntity>();

        var filter = Builders<TEntity>.Filter.Eq("Id", id);

        var entity = await collection.Find(filter).FirstOrDefaultAsync();

        return entity;
    }
    //TODO: doublecheck that this actually returns the entity with the newly generated ID
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var collection = ConnectToMongo<TEntity>();

        await collection.InsertOneAsync(entity);

        return entity;
    }

    public async Task<bool> UpdateAsync(TEntity entity, ObjectId id)
    {
        var collection = ConnectToMongo<TEntity>();


        var filter = Builders<TEntity>.Filter.Eq("Id", id);
        var replaceOptions = new ReplaceOptions { IsUpsert = true };

        await collection.ReplaceOneAsync(filter, entity, replaceOptions);

        return true;
    }

    public async Task<bool> DeleteAsync(ObjectId id)
    {
        var collection = ConnectToMongo<TEntity>();

        var filter = Builders<TEntity>.Filter.Eq("Id", id);

        await collection.DeleteOneAsync(filter);

        return true;
    }
}