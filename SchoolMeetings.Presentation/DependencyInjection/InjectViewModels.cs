using SchoolMeetings.Application.Services.Presentation;
using SchoolMeetings.Domain.Interfaces;
using SchoolMeetings.Presentation.Models.ViewModels.Calendars;
using SchoolMeetings.Presentation.Models.ViewModels.TeacherViewModels;

namespace SchoolMeetings.Presentation.DependencyInjection;

public static class InjectViewModels
{
    public static IServiceCollection AddMyServices(this IServiceCollection services)
    {
        //TODO: Probably remove the calendarViewmodel later since it is only meant to be a baseclass
        services.AddScoped<CalendarViewModel>();
        services.AddScoped<TeacherCalendarViewModel>();
        services.AddScoped<CreateMeetingsViewModel>();

        services.AddScoped<IMeetingService, ClientMeetingService>();

        return services;
    }
}