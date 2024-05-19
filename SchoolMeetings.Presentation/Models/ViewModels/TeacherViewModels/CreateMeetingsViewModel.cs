using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using SchoolMeetings.Presentation.Models.Calendar;
using SchoolMeetings.Presentation.Models.ViewModels.Calendars;
using SchoolMeetings.Presentation.Pages.Calendar;
using SchoolMeetings.Application.Services;
using SchoolMeetings.Application.Services.Presentation;
using SchoolMeetings.Domain.Dtos;
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

    public BookMeetingModel BookMeetingModel { get; set; } = new();
    public Meeting SelectedManualBookMeeting { get; set; } = new();

    public List<Meeting> BookedMeetings { get; set; } = [];
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

    public async Task ManualBookMeetingAsync()
    {

        var dto = new BookMeetingDto
        {
            MeetingId = SelectedManualBookMeeting.Id,
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

        UnBookedMeetings.Remove(SelectedManualBookMeeting);
        BookedMeetings.Add(bookedMeeting);

        //UnBookedMeetings.Remove(SelectedManualBookMeeting);
        //BookedMeetings.Add(SelectedManualBookMeeting);

        SelectedManualBookMeeting = new();
        BookMeetingModel = new();

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

    public async Task CancelMeeting(Meeting meeting)
    {
        var canceledMeeting = await _clientMeetingService.CancelMeeting(meeting);

        if(canceledMeeting is null) 
            return;

        var meetingFromList = BookedMeetings.Find(m => m.Id == canceledMeeting.Id);

        BookedMeetings.Remove(meetingFromList!);
        UnBookedMeetings.Add(canceledMeeting);
    }




}