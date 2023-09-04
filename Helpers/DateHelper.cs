using System.Globalization;

namespace PersonalFinance.Helpers; 
public static class DateHelper {
    public static List<string> GetMonthNamesBetweenDates(DateTime startDate, DateTime endDate)
    {
        List<string> monthNames = new List<string>();

        DateTime currentDate = startDate;

        while (currentDate <= endDate) {
            string monthName = currentDate.ToString("MMMM");
            monthNames.Add(monthName);

            currentDate = currentDate.AddMonths(1);
        }

        return monthNames;
    }
}
