using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinance.Models; 

public enum TransactionStatus { Pending, Completed, Canceled }

public class Transaction {
    [Key]
    public int Id { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O Valor deve ser informado.")]
    public double Amount { get; set; }

    [Range(typeof(DateOnly), "01/01/2000", "01/01/2500", ErrorMessage = "Uma Data posterior a {0} deve ser informada.")]
    public DateOnly Date { get; set; }

    [Required(ErrorMessage = "Descrição deve ser informada.")]
    public string Description { get; set; }

    public TransactionStatus Status { get; set; }

    #region FK
    [ForeignKey(nameof(AccountId))]
    [Range(1, int.MaxValue, ErrorMessage = "Uma Conta deve ser informada.")]
    public int AccountId { get; set; }
    public Account? Account { get; set; }

    [ForeignKey(nameof(TransactionTypeId))]
    [Range(1, int.MaxValue, ErrorMessage = "Um Tipo de Movimentação deve ser informado.")]
    public int TransactionTypeId { get; set; }
    public TransactionType? TransactionType { get; set; }
    #endregion

    public Dictionary<TransactionStatus, string> StatusDictionary =>
        new() {
            {TransactionStatus.Pending, "Pendente" },
            {TransactionStatus.Completed, "Paga" },
            {TransactionStatus.Canceled, "Cancelada" }
        };

    public Transaction()
    {
        Date = DateOnly.FromDateTime(DateTime.Now);
    }
}
