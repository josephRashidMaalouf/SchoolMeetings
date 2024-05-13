using System.Security.Cryptography.X509Certificates;
using SchoolMeetings.Presentation.Models.Calendar;
using SchoolMeetings.Presentation.Models.ViewModels.Calendars;
using SchoolMeetings.Presentation.Pages.Calendar;
using SchoolMeetings.Application.Services;
using SchoolMeetings.Application.Services.Presentation;
using SchoolMeetings.Domain.Entities;
using SchoolMeetings.Domain.Interfaces;

namespace SchoolMeetings.Presentation.Models.ViewModels.TeacherViewModels;

public class CreateMeetingsViewModel(IMeetingService clientMeetingService )
{
    private readonly IMeetingService _clientMeetingService = clientMeetingService;

    public DateTime SelectedDate { get; set; }

    public Meeting NewMeeting { get; set; } = new();
    public List<Meeting> BookedMeetings { get; set; } = new();
    public List<Meeting> UnBookedMeetings { get; set; } = new();



    public async Task FetchMeetingsByTeacher(string email)
    {
        var meetings = await _clientMeetingService.GetAllByTeacherEmailAsync(email);

        if (meetings is null)
            return;

        var bookedMeetings = meetings.Where(m => m.IsBooked is true);
        var unbookedMeetings = meetings.Where(m => m.IsBooked is false);

        BookedMeetings.AddRange(bookedMeetings);
        UnBookedMeetings.AddRange(unbookedMeetings);

    }

    public async Task AddNewMeeting()
    {
        var newlyAddedMeeting = await _clientMeetingService.AddAsync(NewMeeting);

        if(newlyAddedMeeting is null) 
            return;

        UnBookedMeetings.Add(newlyAddedMeeting);
        NewMeeting = new();
    }

    public async Task DeleteMeeting(Meeting meeting)
    {
        var successStatus = await _clientMeetingService.DeleteAsync(meeting.Id);

        if (successStatus is false)
            return;

        if (meeting.IsBooked)
            BookedMeetings.Remove(meeting);
        if (meeting.IsBooked is false)
            UnBookedMeetings.Remove(meeting);
    }

    


}