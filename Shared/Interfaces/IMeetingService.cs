using MongoDB.Bson;
using SchoolMeetings.Domain.Entities;

namespace SchoolMeetings.Domain.Interfaces;

public interface IMeetingService : ICrud<Meeting, string>
{
    Task<ICollection<Meeting>?> GetAllByTeacherEmailAsync(string teacherEmail);
    Task<ICollection<Meeting>?> GetAllByTeacherEmailAndDateAsync(string teacherEmail, string date);
    Task<ICollection<Meeting>?> GetUnbookedByTeacherEmailAsync(string teacherEmail);
    Task<ICollection<Meeting>?> GetBookedByTeacherEmailAsync(string teacherEmail);

}