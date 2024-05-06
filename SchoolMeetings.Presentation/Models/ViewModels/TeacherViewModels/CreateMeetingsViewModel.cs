using System.Security.Cryptography.X509Certificates;
using SchoolMeetings.Presentation.Models.Calendar;
using SchoolMeetings.Presentation.Models.ViewModels.Calendars;
using SchoolMeetings.Presentation.Pages.Calendar;

namespace SchoolMeetings.Presentation.Models.ViewModels.TeacherViewModels;

public class CreateMeetingsViewModel
{

    public DateTime SelectedDate { get; set; }
    public List<MeetingModel> FreeTimeSlots { get; set; } = new();
    public List<MeetingModel> BookedTimeSlots { get; set; } = new();

    //TODO: Create methods for adding, updating and deleting meetings


}