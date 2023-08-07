using PersonalFinance.Models;

namespace PersonalFinance.Data.AutoRegisterLists; 
public class TransactionTypes {
    public static List<TransactionType> GetAll()
    {
        return new() {
            new TransactionType { Description = "Aluguel", TransactionTypeGroupId = 1, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Conta de Luz", TransactionTypeGroupId = 1, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Conta de Água", TransactionTypeGroupId = 1, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Supermercado", TransactionTypeGroupId = 2, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Restaurante", TransactionTypeGroupId = 2, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Combustível", TransactionTypeGroupId = 3, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Transporte Público", TransactionTypeGroupId = 3, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Plano de Saúde", TransactionTypeGroupId = 4, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Medicamentos", TransactionTypeGroupId = 4, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Mensalidade Escolar", TransactionTypeGroupId = 5, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Material Didático", TransactionTypeGroupId = 5, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Cinema e Entretenimento", TransactionTypeGroupId = 6, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Viagens", TransactionTypeGroupId = 6, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Roupas", TransactionTypeGroupId = 7, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Acessórios Pessoais", TransactionTypeGroupId = 7, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Taxas Bancárias", TransactionTypeGroupId = 8, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Juros e Empréstimos", TransactionTypeGroupId = 8, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Imposto de Renda", TransactionTypeGroupId = 9, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "IPTU", TransactionTypeGroupId = 9, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "IPVA", TransactionTypeGroupId = 9, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Doações", TransactionTypeGroupId = 10, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Investimento em Ações", TransactionTypeGroupId = 11, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Investimento em Fundos", TransactionTypeGroupId = 11, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Seguro de Vida", TransactionTypeGroupId = 12, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Seguro Residencial", TransactionTypeGroupId = 12, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Seguro de Carro", TransactionTypeGroupId = 12, Nature = TransactionNature.Outbound },
            new TransactionType { Description = "Salário", TransactionTypeGroupId = 13, Nature = TransactionNature.Inbound },
            new TransactionType { Description = "Reembolso", TransactionTypeGroupId = 13, Nature = TransactionNature.Inbound },
            new TransactionType { Description = "Venda de Produtos", TransactionTypeGroupId = 13, Nature = TransactionNature.Inbound },
            new TransactionType { Description = "Investimento Recebido", TransactionTypeGroupId = 13, Nature = TransactionNature.Inbound },
            new TransactionType { Description = "Pagamento de Dívida Recebido", TransactionTypeGroupId = 13, Nature = TransactionNature.Inbound },
        };
    }
}
