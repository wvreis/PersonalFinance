namespace PersonalFinance.Domain.Entities; 

public class TransactionTypeGroup : BaseEntity
{
    public string Description { get; private set; }

    public TransactionTypeGroup(string description)
    {
        Description = description;
    }
}
