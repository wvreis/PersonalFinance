using PersonalFinance.Helpers;

namespace PersonalFinance.DTOs; 

public class PanelDTO {
    public List<PanelItem>? Incoming { get; set; } = new();
    public List<PanelItem>? Outgoing { get; set; } = new();    

    public PanelDTO() : this(-1) { }

    public PanelDTO(int year = -1)
    {
        if (year == -1) {
            year = DateTime.Now.Year;
        }

        DateTime firstDayOfYear = new DateTime(year, 1, 1);
        DateTime lastDayOfYear = new DateTime(year, 12, 31);

        var monthsList = DateHelper.GetMonthNamesBetweenDates(firstDayOfYear, lastDayOfYear);
        
        monthsList.ForEach(i => Incoming.Add(new(i)));
        monthsList.ForEach(i => Outgoing.Add(new(i)));
    }

    /// <summary>
    /// Sets the Incoming Amount value to given month.
    /// </summary>
    public void SetIncomingItemValue(string month, double amount)
    {
        var incoming = Incoming
            .Where(m => m.Month.Contains(month))
            .Single();
        
        incoming.SetAmount(amount);
    }

    /// <summary>
    /// Sets the Outgoing Amount value to given month.
    /// </summary>
    public void SetOutgoingItemValue(string month, double amount)
    {
        var outgoing = Outgoing
            .Where(m => m.Month.Contains(month))
            .Single();

        outgoing.SetAmount(amount);
    }
}