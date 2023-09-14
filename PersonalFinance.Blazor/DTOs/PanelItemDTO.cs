namespace PersonalFinance.Blazor.DTOs;

public class PanelItem {
    public string Month { get; set; }
    public double Amount { get; set; }

    public PanelItem() { }

    public PanelItem(string month, double amount = 0)
    {
        Month = month;
        Amount = amount;
    }

    public void SetAmount(double amount)
    {
        Amount = amount;
    }
}