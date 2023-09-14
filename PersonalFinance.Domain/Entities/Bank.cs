namespace PersonalFinance.Domain.Entities;

public class Bank
{
    public string Name { get; private set; }
    public int Number { get; private set; }
    public List<Account> Accounts { get; private set; } = new();

    public Bank(
        string name,
        int number,
        List<Account> accounts)
    {
        Name = name;
        Number = number;
        Accounts = accounts;
    }
}