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

    public async Task<ICollection<Meeting>> GetAllByTeacherEmailAndDateAsync(string teacherEmail, string date)
    {
        var collection = ConnectToMongo<Meeting>();

        DateTime parsedDate = DateTime.Parse(date);
        DateTime startDate = parsedDate.Date;
        DateTime endDate = startDate.AddDays(1); // Add one day to include the whole day

        var dateFilter = Builders<Meeting>.Filter.Gte("MeetingStart", startDate) &
                         Builders<Meeting>.Filter.Lt("MeetingStart", endDate);

        var teacherFilter = Builders<Meeting>.Filter.Eq("TeacherEmail", teacherEmail);

        var filter = Builders<Meeting>.Filter.And(teacherFilter, dateFilter);

        var results = await collection.FindAsync(filter);

        return await results.ToListAsync();
    }

    public async Task<ICollection<Meeting>?> GetUnBookedByTeacherEmailAndMonthAsync(string teacherEmail, string date)
    {
        var collection = ConnectToMongo<Meeting>();

        DateTime parsedDate = DateTime.Parse(date);
        var year = parsedDate.Year;
        var month = parsedDate.Month;
        var monthAndYearDateStart = new DateTime(year, month, 1);
        var monthAndYearDateEnd = monthAndYearDateStart.AddMonths(1);

        var dateFilter = Builders<Meeting>.Filter.Gte("MeetingStart", monthAndYearDateStart) &
                         Builders<Meeting>.Filter.Lt("MeetingStart", monthAndYearDateEnd);

        var teacherFilter = Builders<Meeting>.Filter.Eq("TeacherEmail", teacherEmail);

        var unbookedFilter = Builders<Meeting>.Filter.Eq("IsBooked", false);

        var filter = Builders<Meeting>.Filter.And(teacherFilter, dateFilter, unbookedFilter);

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

    public async Task<ICollection<Meeting>> GetAllByTeacherEmailAndMonthAsync(string teacherEmail, string date)
    {
        var collection = ConnectToMongo<Meeting>();

        DateTime parsedDate = DateTime.Parse(date);
        var year = parsedDate.Year;
        var month = parsedDate.Month;
        var monthAndYearDateStart = new DateTime(year, month, 1);
        var monthAndYearDateEnd = monthAndYearDateStart.AddMonths(1);

        var dateFilter = Builders<Meeting>.Filter.Gte("MeetingStart", monthAndYearDateStart) &
                         Builders<Meeting>.Filter.Lt("MeetingStart", monthAndYearDateEnd);

        var teacherFilter = Builders<Meeting>.Filter.Eq("TeacherEmail", teacherEmail);

        var filter = Builders<Meeting>.Filter.And(teacherFilter, dateFilter);

        var results = await collection.FindAsync(filter);

        return await results.ToListAsync();
    }
}