using MongoDB.Bson;
using SchoolMeetings.Domain.Entities;
using SchoolMeetings.Domain.Interfaces;


namespace SchoolMeetings.Application.Services.Api;

public class ApiMeetingService(IMeetingRepository metingRepository) : IMeetingService
{
    private readonly IMeetingRepository _metingRepository = metingRepository;

    public async Task<ICollection<Meeting>> GetAllAsync()
    {
        var meetings = await _metingRepository.GetAllAsync();
        return meetings;
    }

    public async Task<Meeting?> GetByIdAsync(ObjectId id)
    {
        var meeting = await _metingRepository.GetByIdAsync(id);
        return meeting;
    }

    public async Task<Meeting> AddAsync(Meeting entity)
    {
        var newlyAddedMeeting = await _metingRepository.AddAsync(entity);

        return newlyAddedMeeting;
    }

    public async Task<bool> UpdateAsync(Meeting entity)
    {
        var updateSuccess = await _metingRepository.UpdateAsync(entity, entity.Id);

        return updateSuccess;
    }

    public async Task<bool> DeleteAsync(ObjectId id)
    {
        var deleteSuccess = await _metingRepository.DeleteAsync(id);

        return deleteSuccess;
    }

    public async Task<ICollection<Meeting>> GetAllByTeacherEmailAsync(string teacherEmail)
    {
        var meetings = await _metingRepository.GetAllByTeacherEmailAsync(teacherEmail);

        return meetings;
    }

    public async Task<ICollection<Meeting>> GetUnbookedByTeacherEmailAsync(string teacherEmail)
    {
        var meetings = await _metingRepository.GetUnbookedByTeacherEmailAsync(teacherEmail);

        return meetings;
    }

    public async Task<ICollection<Meeting>> GetBookedByTeacherEmailAsync(string teacherEmail)
    {
        var meetings = await _metingRepository.GetBookedByTeacherEmailAsync(teacherEmail);

        return meetings;
    }
}