using SchoolMeetings.Api.Extensions;

namespace SchoolMeetings.Api.DependencyInjection;

public static class MapEndPoints
{
    public static IEndpointRouteBuilder MapAllEndPoints(this IEndpointRouteBuilder app)
    {
        app.MapCustomAuthenticationEndPoints();


        return app;
    }
}