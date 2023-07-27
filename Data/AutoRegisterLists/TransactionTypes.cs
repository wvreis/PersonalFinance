using PersonalFinance.Models;

namespace PersonalFinance.Data.AutoRegisterLists; 
public class TransactionTypes {
    public static List<TransactionType> GetAll()
    {
        return new() {
            new TransactionType { Description = "Aluguel", TransactionTypeGroupId = 1 },
            new TransactionType { Description = "Conta de Luz", TransactionTypeGroupId = 1 },
            new TransactionType { Description = "Conta de Água", TransactionTypeGroupId = 1 },
            new TransactionType { Description = "Supermercado", TransactionTypeGroupId = 2 },
            new TransactionType { Description = "Restaurante", TransactionTypeGroupId = 2 },
            new TransactionType { Description = "Combustível", TransactionTypeGroupId = 3 },
            new TransactionType { Description = "Transporte Público", TransactionTypeGroupId = 3 },
            new TransactionType { Description = "Plano de Saúde", TransactionTypeGroupId = 4 },
            new TransactionType { Description = "Medicamentos", TransactionTypeGroupId = 4 },
            new TransactionType { Description = "Mensalidade Escolar", TransactionTypeGroupId = 5 },
            new TransactionType { Description = "Material Didático", TransactionTypeGroupId = 5 },
            new TransactionType { Description = "Cinema e Entretenimento", TransactionTypeGroupId = 6 },
            new TransactionType { Description = "Viagens", TransactionTypeGroupId = 6 },
            new TransactionType { Description = "Roupas", TransactionTypeGroupId = 7 },
            new TransactionType { Description = "Acessórios Pessoais", TransactionTypeGroupId = 7 },
            new TransactionType { Description = "Taxas Bancárias", TransactionTypeGroupId = 8 },
            new TransactionType { Description = "Juros e Empréstimos", TransactionTypeGroupId = 8 },
            new TransactionType { Description = "Imposto de Renda", TransactionTypeGroupId = 9 },
            new TransactionType { Description = "IPTU", TransactionTypeGroupId = 9 },
            new TransactionType { Description = "IPVA", TransactionTypeGroupId = 9 },
            new TransactionType { Description = "Doações", TransactionTypeGroupId = 10 },
            new TransactionType { Description = "Investimento em Ações", TransactionTypeGroupId = 11 },
            new TransactionType { Description = "Investimento em Fundos", TransactionTypeGroupId = 11 },
            new TransactionType { Description = "Seguro de Vida", TransactionTypeGroupId = 12 },
            new TransactionType { Description = "Seguro Residencial", TransactionTypeGroupId = 12 },
            new TransactionType { Description = "Seguro de Carro", TransactionTypeGroupId = 12 }
        };
    }
}
