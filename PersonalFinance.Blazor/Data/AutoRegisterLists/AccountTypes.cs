using PersonalFinance.Blazor.Models;

namespace PersonalFinance.Blazor.Data.AutoRegisterLists;
public class AccountTypes {
    public static List<AccountType> GetAll()
    {
        return new() {
            new AccountType { Description = "Carteira" },
            new AccountType { Description = "Conta Corrente" },
            new AccountType { Description = "Poupança" },
            new AccountType { Description = "Outros" }
        };
    }
}