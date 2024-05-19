using SchoolMeetings.Domain.Dtos;
using SchoolMeetings.Domain.Entities;
using SchoolMeetings.Domain.Interfaces;
using SchoolMeetings.Presentation.Models.Calendar;

namespace SchoolMeetings.Presentation.Models.ViewModels.ParentViewModels;

public class BookMeetingViewModel(IMeetingService clientMeetingService)
{
    private readonly IMeetingService _clientMeetingService = clientMeetingService;

    public List<Meeting> UnbookedMeetings { get; set; } = [];
    public Meeting SelectedMeeting { get; set; } = new();
    public BookMeetingModel BookMeetingModel { get; set; } = new();


    public async Task FetchUnbookedMeetings(string teacherEmail)
    {
        UnbookedMeetings.Clear();

        var meetings =
            await _clientMeetingService
                .GetUnbookedByTeacherEmailAsync(teacherEmail);

        //TODO: Let user know something went wrong
        if (meetings is null)
            return;

        //TODO: This filtering by dates beyond the date of today should be refactored to its own endpoint
        UnbookedMeetings.AddRange(meetings.Where(m => m.MeetingStart >= DateTime.UtcNow));
    }

    public async Task BookMeeting()
    {
        var dto = new BookMeetingDto
        {
            MeetingId = SelectedMeeting.Id,
            ParentName1 = BookMeetingModel.ParentName1
        };
        if (string.IsNullOrWhiteSpace(BookMeetingModel.ParentEmail1) is false)
            dto.ParentEmail1 = BookMeetingModel.ParentEmail1;
        if (string.IsNullOrWhiteSpace(BookMeetingModel.ParentPhone1) is false)
            dto.ParentPhone1 = BookMeetingModel.ParentPhone1;

        if (string.IsNullOrWhiteSpace(BookMeetingModel.ParentName2) is false)
            dto.ParentName2 = BookMeetingModel.ParentName2;
        if (string.IsNullOrWhiteSpace(BookMeetingModel.ParentEmail2) is false)
            dto.ParentEmail2 = BookMeetingModel.ParentEmail2;
        if (string.IsNullOrWhiteSpace(BookMeetingModel.ParentPhone2) is false)
            dto.ParentPhone2 = BookMeetingModel.ParentPhone2;

        if (string.IsNullOrWhiteSpace(BookMeetingModel.NameOfStudent) is false)
            dto.NameOfStudent = BookMeetingModel.NameOfStudent;

        var bookedMeeting = await _clientMeetingService.BookMeeting(dto);

        //TODO: Let user know update failed
        if (bookedMeeting is null)
            return;

        UnbookedMeetings.Remove(SelectedMeeting);


        SelectedMeeting = new();
        BookMeetingModel = new();

        //TODO: Implement email confirmation to parents emailadress and let user know booking succeeded
    }
}