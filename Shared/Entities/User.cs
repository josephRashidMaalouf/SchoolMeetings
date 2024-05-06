using Microsoft.AspNetCore.Identity;
using SchoolMeetings.Domain.Interfaces;

namespace SchoolMeetings.Domain.Entities;

public class User : IdentityUser, IEntity<string>
{
    
}