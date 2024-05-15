using SchoolMeetings.Domain.Entities;

namespace SchoolMeetings.Domain.Interfaces;

public interface IClientMeetingService : IMeetingService
{
    Task<bool> CancelMeeting(string meetingId);
}