namespace PersonalFinance.Domain.Entities; 

public class TransactionTypeGroup : BaseEntity
{
    public string Description { get; private set; }
    public List<TransactionType> TransactionTypes { get; private set; }

    public TransactionTypeGroup(string description, List<TransactionType> transactionTypes)
    {
        Description = description;
        TransactionTypes = transactionTypes;
    }
}
