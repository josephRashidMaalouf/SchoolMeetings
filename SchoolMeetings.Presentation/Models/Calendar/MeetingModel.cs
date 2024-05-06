using SchoolMeetings.Presentation.Models.People;

namespace SchoolMeetings.Presentation.Models.Calendar;

public class MeetingModel
{
    public TeacherModel? Teacher { get; set; }
    public List<ParentModel>? Parents { get; set; }
    public DateTime MeetingStart { get; set; }
    public DateTime MeetingEnd { get; set; }
    public bool IsBooked { get; set; } = false;
}