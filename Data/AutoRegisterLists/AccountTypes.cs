using PersonalFinance.Models;

namespace PersonalFinance.Data.AutoRegisterLists;
public class AccountTypes
{
    public static List<AccountType> All
    {
        get => new() {
            new AccountType { Description = "Carteira" },
            new AccountType { Description = "Conta Corrente" },
            new AccountType { Description = "Poupança" },
            new AccountType { Description = "Outros" }
        };
    }
}
