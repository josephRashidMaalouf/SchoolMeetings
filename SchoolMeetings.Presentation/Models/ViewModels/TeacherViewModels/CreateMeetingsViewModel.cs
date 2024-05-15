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
    public string LoggedInTeacherEmail { get; set; } = string.Empty;
    public Meeting NewMeeting { get; set; } = new();
    public TimeOnly NewMeetingStart { get; set; } = new();
    public TimeOnly NewMeetingEnd { get; set; } = new();

    public Meeting SelectedEditMeeting { get; set; } = new();

    public List<Meeting> BookedMeetings { get; set; } = new();
    public List<Meeting> UnBookedMeetings { get; set; } = new();



    public async Task FetchMeetingsByTeacher(string email)
    {
        var meetings = await _clientMeetingService.GetAllByTeacherEmailAndDateAsync(email, SelectedDate.ToShortDateString());

        if (meetings is null)
            return;

        var bookedMeetings = meetings.Where(m => m.IsBooked is true);
        var unbookedMeetings = meetings.Where(m => m.IsBooked is false);

        BookedMeetings.Clear();
        UnBookedMeetings.Clear();

        BookedMeetings.AddRange(bookedMeetings);
        UnBookedMeetings.AddRange(unbookedMeetings);

    }

    
    public async Task AddNewMeetingAsync(string email)
    {
        NewMeeting.TeacherEmail = email;
        NewMeeting.MeetingStart = SelectedDate;
        NewMeeting.MeetingEnd = SelectedDate;

        NewMeeting.MeetingStart += NewMeetingStart.ToTimeSpan();
        NewMeeting.MeetingEnd += NewMeetingEnd.ToTimeSpan();


        var newlyAddedMeeting = await _clientMeetingService.AddAsync(NewMeeting);

        if(newlyAddedMeeting is null) 
            return;

        UnBookedMeetings.Add(newlyAddedMeeting);
        NewMeeting = new();
    }

    public async Task UpdateMeetingAsync(Meeting meeting)
    {

    }

    public async Task DeleteMeetingAsync(Meeting meeting)
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