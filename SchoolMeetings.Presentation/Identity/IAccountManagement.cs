using SchoolMeetings.Presentation.Identity.Models;

namespace SchoolMeetings.Presentation.Identity;

public interface IAccountManagement
{
	public Task<AuthResponseModel> LoginAsync(string email, string password);

	public Task LogoutAsync();

	public Task<AuthResponseModel> RegisterAsync(string email, string password);

	public Task<bool> CheckAuthenticatedAsync();
}
