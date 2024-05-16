using MongoDB.Bson;
using SchoolMeetings.Domain.Entities;

namespace SchoolMeetings.Domain.Interfaces;

public interface IMeetingRepository : IMongoRepository<Meeting>
{
    Task<ICollection<Meeting>> GetAllByTeacherEmailAsync(string teacherEmail);
    Task<ICollection<Meeting>> GetAllByTeacherEmailAndDateAsync(string teacherEmail, string date);
    Task<ICollection<Meeting>?> GetUnBookedByTeacherEmailAndMonthAsync(string teacherEmail, string date);
    Task<ICollection<Meeting>> GetUnbookedByTeacherEmailAsync(string teacherEmail);
    Task<ICollection<Meeting>> GetBookedByTeacherEmailAsync(string teacherEmail);
    Task<ICollection<Meeting>> GetAllByTeacherEmailAndMonthAsync(string teacherEmail, string date);
}