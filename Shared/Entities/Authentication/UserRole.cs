using Microsoft.AspNetCore.Identity;

namespace SchoolMeetings.Domain.Entities.Authentication;

public class UserRole : IdentityUserRole<string>
{
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}