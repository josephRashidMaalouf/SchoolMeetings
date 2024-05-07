using MongoDB.Bson;
using MongoDB.Driver;
using SchoolMeetings.Domain.Entities;
using SchoolMeetings.Domain.Interfaces;

namespace SchoolMeetings.Infrastructure.Repositories.Mongo;

public class MeetingsRepository(string collectionName) : MongoRepositoryBase<Meeting>(collectionName), IMeetingRepository
{
    public async Task<ICollection<Meeting>> GetAllByTeacherEmailAsync(string teacherEmail)
    {
        var collection = ConnectToMongo<Meeting>();

        var filter = Builders<Meeting>.Filter.Eq("TeacherEmail", teacherEmail);

        var results = await collection.FindAsync(filter);

        return await results.ToListAsync();
    }
    public async Task<ICollection<Meeting>> GetUnbookedByTeacherEmailAsync(string teacherEmail)
    {
        var collection = ConnectToMongo<Meeting>();

        var teacherFilter = Builders<Meeting>.Filter.Eq("TeacherEmail", teacherEmail);
        var isBookedFilter = Builders<Meeting>.Filter.Eq("IsBooked", false);

        var filter = Builders<Meeting>.Filter.And(teacherFilter, isBookedFilter);

        var results = await collection.FindAsync(filter);

        return await results.ToListAsync();
    }
    public async Task<ICollection<Meeting>> GetBookedByTeacherEmailAsync(string teacherEmail)
    {
        var collection = ConnectToMongo<Meeting>();

        var teacherFilter = Builders<Meeting>.Filter.Eq("TeacherEmail", teacherEmail);
        var isBookedFilter = Builders<Meeting>.Filter.Eq("IsBooked", true);

        var filter = Builders<Meeting>.Filter.And(teacherFilter, isBookedFilter);

        var results = await collection.FindAsync(filter);

        return await results.ToListAsync();
    }


}