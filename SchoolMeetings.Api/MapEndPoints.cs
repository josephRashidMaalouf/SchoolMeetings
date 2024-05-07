using SchoolMeetings.Api.Extensions;

namespace SchoolMeetings.Api;

public static class MapEndPoints
{
    public static IEndpointRouteBuilder MapAllEndPoints(this IEndpointRouteBuilder app)
    {
        app.MapCustomAuthenticationEndPoints();
        app.MapMeetingEndPoints();

        return app;
    }
}