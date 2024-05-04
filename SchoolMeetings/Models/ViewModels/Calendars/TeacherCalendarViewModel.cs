using Microsoft.AspNetCore.Components;
using Shared.Managers;

namespace Shared.Models.ViewModels.Calendars;

public class TeacherCalendarViewModel(NavigationManager navigationManager) : CalendarViewModel
{
    public async Task PickDateToCreateMeeting(int day)
    {
        navigationManager.NavigateTo("creating-meetings");
    }
}