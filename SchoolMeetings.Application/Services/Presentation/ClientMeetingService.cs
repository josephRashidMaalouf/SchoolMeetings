using MongoDB.Bson;
using SchoolMeetings.Domain.Entities;
using SchoolMeetings.Domain.Interfaces;

namespace SchoolMeetings.Application.Services.Presentation;

public class ClientMeetingService : IMeetingService
{
    public Task<ICollection<Meeting>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Meeting?> GetByIdAsync(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public Task<Meeting> AddAsync(Meeting entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Meeting entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Meeting>> GetAllByTeacherEmailAsync(string teacherEmail)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Meeting>> GetUnbookedByTeacherEmailAsync(string teacherEmail)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Meeting>> GetBookedByTeacherEmailAsync(string teacherEmail)
    {
        throw new NotImplementedException();
    }
}