using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolMeetings.Domain.Entities.Authentication;

namespace SchoolMeetings.Infrastructure.DevelopmentUserDemoData;

public class SeedData
{
    private static readonly IEnumerable<SeedUser> seedUsers =
    [
        new SeedUser()
        {
            Email = "teacher@teacher.teacher",
            NormalizedEmail = "TEACHER@TEACHER.TEACHER",
            NormalizedUserName = "TEACHER@TEACHER.TEACHER",
            RoleList = ["Teacher"],
            UserName = "teacher@teacher.teacher"
        },
        new SeedUser()
        {
            Email = "parent@parent.parent",
            NormalizedEmail = "PARENT@PARENT.PARENT",
            NormalizedUserName = "PARENT@PARENT.PARENT",
            RoleList = ["Parent"],
            UserName = "parent@parent.parent"
        },
    ];


    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var context = new SchoolMeetingsDbContext(serviceProvider.GetRequiredService<DbContextOptions<SchoolMeetingsDbContext>>());

        if (context.Users.Any())
        {
            return;
        }

        var userStore = new UserStore<User>(context);
        var password = new PasswordHasher<User>();

        using var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

        string[] roles = ["Teacher", "Parent"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new Role(role));
            }
        }

        using var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        foreach (var user in seedUsers)
        {
            var hashed = password.HashPassword(user, "Passw0rd!");
            user.PasswordHash = hashed;
            await userStore.CreateAsync(user);

            if (user.Email is not null)
            {
                var appUser = await userManager.FindByEmailAsync(user.Email);

                if (appUser is not null && user.RoleList is not null)
                {
                    await userManager.AddToRolesAsync(appUser, user.RoleList);
                }
            }
        }
        

        await context.SaveChangesAsync();
    }

    private class SeedUser : User
    {
        public string[]? RoleList { get; set; }
    }
}