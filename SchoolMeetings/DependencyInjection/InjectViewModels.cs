using Shared.Models.ViewModels.Calendars;

namespace SchoolMeetings.Presentation.DependencyInjection;

public static class InjectViewModels
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddScoped<TeacherCalendarViewModel>();

        return services;
    }
}