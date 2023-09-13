namespace PersonalFinance.Domain.Entities; 

public class Account : BaseEntity 
{
    public string Description { get; private set; }
    public double OpeningBalance { get; private set; }
    public bool Status { get; private set; }
    public int BankId { get; private set; }
    public int AccountTypeId { get; private set; }

    public Account(
        string description,
        double openingBalance,
        bool status,
        int bankId,
        int accountTypeId)
    {
        Description = description;
        OpeningBalance = openingBalance;
        Status = status;
        BankId = bankId;
        AccountTypeId = accountTypeId;
    }
}
