using System.Security.Cryptography.X509Certificates;
using Shared.Managers;

namespace Shared.Models.ViewModels.TeacherViewModels;

public class CreateMeetingsViewModel
{
    

    public CreateMeetingsViewModel()
    {
        MeetingManager.DatePickedToCreateMeeting += MeetingManager_DatePickedToCreateMeeting;
    }

    private void MeetingManager_DatePickedToCreateMeeting(Calendar.MonthModel month, int date)
    {
        
        throw new NotImplementedException();
    }
}