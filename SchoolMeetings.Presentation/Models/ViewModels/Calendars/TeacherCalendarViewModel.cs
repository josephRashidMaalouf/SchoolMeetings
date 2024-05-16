using Microsoft.AspNetCore.Components;
using SchoolMeetings.Application.Services.Presentation;
using SchoolMeetings.Domain.Interfaces;
using Shared.Enums;
using Shared.Models.Calendar;

namespace SchoolMeetings.Presentation.Models.ViewModels.Calendars;

public class TeacherCalendarViewModel(NavigationManager navigationManager, IMeetingService meetingService) : CalendarViewModel
{
    private readonly IMeetingService _meetingService = meetingService;

    public string LoggedInTeacherEmail { get; set; } = string.Empty;


    public async Task PickDateToCreateMeeting(int day)
    {
        var month = (int)Enum.Parse(typeof(MonthNames), Month.MonthName);
        var monthString = month.ToString();
        if (monthString.Length < 2)
            monthString = "0" + month;

        var datesString = $"{Month.Year}-{monthString}-{day}";
           
        navigationManager.NavigateTo($"create-meetings/{datesString}");
    }

    public override async Task GenerateMonth(int monthsAwayFromNow)
    {
        await base.GenerateMonth(monthsAwayFromNow);

        var monthString = new DateTime(int.Parse(Month.Year), Month.MonthNumber, 1).ToShortDateString();

        var meetings = await _meetingService.GetAllByTeacherEmailAndMonthAsync(LoggedInTeacherEmail, monthString);

        var dateOfMeetings = meetings.Select(m => m.MeetingStart.Day);

        Month.BookableDates.AddRange(dateOfMeetings);
    }
}