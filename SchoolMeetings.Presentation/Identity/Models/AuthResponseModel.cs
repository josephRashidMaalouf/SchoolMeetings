namespace SchoolMeetings.Presentation.Identity.Models;

public class AuthResponseModel
{
	public bool Succeeded { get; set; }

	public string[] ErrorList { get; set; } = [];
}
