using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using PersonalFinance.Models;

namespace PersonalFinance.Data.AutoRegisterLists; 
public class TransactionTypeGroups {
    public static List<TransactionTypeGroup> GetAll()
    {
        return new() {
            new TransactionTypeGroup { Id = 1, Description = "Habitação" },
            new TransactionTypeGroup { Id = 2, Description = "Alimentação" },
            new TransactionTypeGroup { Id = 3, Description = "Transporte" },
            new TransactionTypeGroup { Id = 4, Description = "Saúde" },
            new TransactionTypeGroup { Id = 5, Description = "Educação" },
            new TransactionTypeGroup { Id = 6, Description = "Lazer" },
            new TransactionTypeGroup { Id = 7, Description = "Vestuário" },
            new TransactionTypeGroup { Id = 8, Description = "Finanças" },
            new TransactionTypeGroup { Id = 9, Description = "Impostos" },
            new TransactionTypeGroup { Id = 10, Description = "Doações e Caridade" },
            new TransactionTypeGroup { Id = 11, Description = "Investimentos" },
            new TransactionTypeGroup { Id = 12, Description = "Seguro" },
            new TransactionTypeGroup { Id = 13, Description = "Receitas"}
        };
    }
}