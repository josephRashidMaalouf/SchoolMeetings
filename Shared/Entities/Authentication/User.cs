using Microsoft.AspNetCore.Identity;
using SchoolMeetings.Domain.Interfaces;

namespace SchoolMeetings.Domain.Entities.Authentication;

public class User : IdentityUser, IEntity<string>
{
    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}