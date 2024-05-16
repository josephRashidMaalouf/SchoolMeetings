using Shared.Models.Calendar;

namespace SchoolMeetings.Presentation.Models.Calendar;

public class MonthModel
{
    public string MonthName { get; set; } = string.Empty;
    public int MonthNumber { get; set; }
    public DateTime MonthEnd { get; set; }
    public string Year { get; set; } = string.Empty;
    public int NumOfDummyColumns { get; set; }
    public DateTime DateOfToday { get; set; }
    public List<int> BookableDates { get; set; } = new();
    public List<DayModel> Days { get; set; } = new();

}