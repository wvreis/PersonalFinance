using PersonalFinance.Models;

namespace PersonalFinance.Data.AutoRegisterLists;
public class Banks
{
    public static List<Bank> GetAll()
    {
        return new()
        {
            new Bank { Name = "Banco do Brasil", Number = 1 },
            new Bank { Name = "Banco Bradesco", Number = 237 },
            new Bank { Name = "Caixa Econômica Federal", Number = 104 },
            new Bank { Name = "Itaú Unibanco", Number = 341 },
            new Bank { Name = "Santander", Number = 33 },
            new Bank { Name = "Banco Safra", Number = 422 },
            new Bank { Name = "Banco BTG Pactual", Number = 208 },
            new Bank { Name = "Banco Inter", Number = 77 },
            new Bank { Name = "Nubank", Number = 260 },
            new Bank { Name = "Banrisul", Number = 041 },
            new Bank { Name = "Banco Votorantim", Number = 655 },
            new Bank { Name = "Banco do Nordeste", Number = 004 },
            new Bank { Name = "Banestes", Number = 021 },
            new Bank { Name = "Banco Daycoval", Number = 707 },
            new Bank { Name = "Banco Alfa", Number = 25 },
            new Bank { Name = "Banco Original", Number = 212 },
            new Bank { Name = "Banco Pine", Number = 643 },
            new Bank { Name = "Banco Sofisa", Number = 637 },
        };
    }
}
