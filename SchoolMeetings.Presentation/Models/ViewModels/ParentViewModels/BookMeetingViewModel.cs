using SchoolMeetings.Domain.Entities;
using SchoolMeetings.Domain.Interfaces;

namespace SchoolMeetings.Presentation.Models.ViewModels.ParentViewModels;

public class BookMeetingViewModel(IMeetingService clientMeetingService)
{
    private readonly IMeetingService _clientMeetingService = clientMeetingService;

    public List<Meeting> UnbookedMeetings { get; set; } = [];

    public async Task FetchUnbookedMeetings(string teacherEmail)
    {
        var meetings =
            await _clientMeetingService
                .GetUnbookedByTeacherEmailAsync(teacherEmail);

        //TODO: Let user know something went wrong
        if (meetings is null)
            return;

        //TODO: This filtering by dates beyond the date of today should be refactored to its own endpoint
        UnbookedMeetings.AddRange(meetings.Where(m => m.MeetingStart >= DateTime.UtcNow));
    }
}