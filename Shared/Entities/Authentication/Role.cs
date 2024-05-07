using Microsoft.AspNetCore.Identity;

namespace SchoolMeetings.Domain.Entities.Authentication;

public class Role : IdentityRole
{
    public Role() { }

    public Role(string roleName) : base(roleName) { }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}