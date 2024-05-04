using Microsoft.AspNetCore.Components;
using Shared.Enums;
using Shared.Models.Calendar;

namespace SchoolMeetings.Presentation.Models.ViewModels.Calendars;

public class TeacherCalendarViewModel(NavigationManager navigationManager) : CalendarViewModel
{
    public DayModel SelectedDate { get; set; } = new();
    public async Task PickDateToCreateMeeting(int day)
    {
        var month = Enum.Parse(typeof(MonthNames), Month.MonthName);
        SelectedDate.Month = (int)month;
        SelectedDate.Year = Month.Year;
        SelectedDate.Day = day;
        navigationManager.NavigateTo($"creating-meetings/{SelectedDate}");
    }
}