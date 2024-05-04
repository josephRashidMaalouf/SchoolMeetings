using Microsoft.AspNetCore.Components;
using Shared.Enums;
using Shared.Models.Calendar;

namespace SchoolMeetings.Presentation.Models.ViewModels.Calendars;

public class TeacherCalendarViewModel(NavigationManager navigationManager) : CalendarViewModel
{
    public async Task PickDateToCreateMeeting(int day)
    {
        var month = (int)Enum.Parse(typeof(MonthNames), Month.MonthName);
        var monthString = month.ToString();
        if (monthString.Length < 2)
            monthString = "0" + month;

        var datesString = $"{Month.Year}-{monthString}-{day}";
           
        navigationManager.NavigateTo($"create-meetings/{datesString}");
    }
}