namespace PersonalFinance.Domain.Entities;

public class Transaction : BaseEntity
{
    public double Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public TransactionStatus Status { get; private set; }
    public TransactionNature Nature { get; private set; }
    public int AccountId { get; private set; }
    public int TransactionTypeId { get; private set; }

    public Transaction(
        double amount,
        DateTime date,
        string description,
        TransactionStatus status,
        TransactionNature nature,
        int accountId,
        int transactionTypeId)
    {
        Amount = amount;
        Date = date;
        Description = description;
        Status = status;
        Nature = nature;
        AccountId = accountId;
        TransactionTypeId = transactionTypeId;
    }
}
