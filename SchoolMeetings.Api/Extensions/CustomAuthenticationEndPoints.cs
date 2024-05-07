using Microsoft.AspNetCore.Identity;
using SchoolMeetings.Domain.Entities.Authentication;
using System.Security.Claims;

namespace SchoolMeetings.Api.Extensions;

public static class CustomAuthenticationEndPoints
{
    public static IEndpointRouteBuilder MapCustomAuthenticationEndPoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/user");

        app.MapPost("/logout", LogOut).RequireAuthorization();

        app.MapGet("/roles", GetRoles).RequireAuthorization();


        return app;
    }

    public static async Task<IResult> LogOut(SignInManager<User> signInManager, object empty)
    {
        if (empty is not null)
        {
            await signInManager.SignOutAsync();

            return Results.Ok();
        }

        return Results.Unauthorized();
    }

    public static async Task<IResult> GetRoles(ClaimsPrincipal user)
    {
        if (user.Identity is not null && user.Identity.IsAuthenticated)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var roles = identity.FindAll(identity.RoleClaimType)
                .Select(c =>
                    new
                    {
                        c.Issuer,
                        c.OriginalIssuer,
                        c.Type,
                        c.Value,
                        c.ValueType
                    });

            return TypedResults.Json(roles);
        }

        return Results.Unauthorized();
    }
}