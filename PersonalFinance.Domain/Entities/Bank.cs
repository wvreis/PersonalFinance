namespace PersonalFinance.Domain.Entities;

public class Bank
{
    public string Name { get; private set; }
    public int Number { get; private set; }

    public Bank(
        string name,
        int number)
    {
        Name = name;
        Number = number;
    }
}