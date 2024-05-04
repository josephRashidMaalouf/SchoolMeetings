using SchoolMeetings.Presentation.Models.ViewModels.Calendars;

namespace SchoolMeetings.Presentation.DependencyInjection;

public static class InjectViewModels
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        //TODO: Probably remove the calendarViewmodel later since it is only meant to be a baseclass
        services.AddScoped<CalendarViewModel>();
        services.AddScoped<TeacherCalendarViewModel>();

        return services;
    }
}