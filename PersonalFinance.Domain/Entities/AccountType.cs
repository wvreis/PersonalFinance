namespace PersonalFinance.Domain.Entities; 

public class AccountType : BaseEntity
{
    public string Description { get; private set; }
    public bool Status { get; private set; }
    public List<Account>? Accounts { get; private set; }

    public AccountType(
        string description,
        bool status,
        List<Account>? accounts)
    {
        Description = description;
        Status = status;
        Accounts = accounts;
    }
}
