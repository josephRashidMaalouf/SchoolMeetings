using SchoolMeetings.Application.Services.Api;
using SchoolMeetings.Domain.Interfaces;
using SchoolMeetings.Infrastructure.Repositories.Mongo;

namespace SchoolMeetings.Api.DependencyInjection;

public static class InjectApiServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {

        services.AddScoped<IMeetingRepository, MeetingsRepository>(provider => new MeetingsRepository("Meetings"));

        services.AddScoped<IMeetingService, ApiMeetingService>();

        return services;
    }
}