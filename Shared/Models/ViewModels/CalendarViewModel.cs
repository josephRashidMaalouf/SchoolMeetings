using Shared.Enums;
using Shared.Models.Calendar;

namespace Shared.Models.ViewModels;

public class CalendarViewModel
{
    public MonthModel Month { get; set; } = new();
    public int MonthsAway { get; set; } = 0;


    public async Task GenerateMonth(int monthsAwayFromNow)
    {
        Month.Year = DateTime.UtcNow.AddMonths(monthsAwayFromNow).Year.ToString();

        Month.DateOfToday = DateTime.UtcNow.Date;

        DateTime monthStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1)
            .AddMonths(MonthsAway);
        Month.MonthEnd = monthStart
            .AddMonths(1)
        .AddDays(-1);

        Month.MonthName = Enum.GetName(typeof(MonthNames), monthStart.Month)!;

        Month.NumOfDummyColumns = (int)monthStart.DayOfWeek;

        Month.BookableDates.AddRange([1, 5, 3]);
    }

    public async Task GoToPrevMonth()
    {
        MonthsAway--;
        await GenerateMonth(MonthsAway);
    }

    public async Task GoToNextMonth()
    {
        MonthsAway++;
        await GenerateMonth(MonthsAway);
    }
}