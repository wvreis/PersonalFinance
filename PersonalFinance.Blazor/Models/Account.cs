using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinance.Blazor.Models; 

public class Account {    
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "A Descrição precisa ser informada.")]
    [StringLength(200)]
    public string Description { get; set; }

    public double OpeningBalance { get; set; }

    public bool Status { get; set; } = true;

    #region FK
    [ForeignKey(nameof(BankId))]
    [Range(1, int.MaxValue, ErrorMessage = "Um Banco deve ser selecionado.")]
    public int BankId { get; set; }
    public Bank? Bank { get; set; }

    [ForeignKey(nameof(AccountTypeId))]
    [Range(1, int.MaxValue, ErrorMessage = "O Tipo de Conta deve ser informado.")]
    public int AccountTypeId { get; set; }
    public AccountType? AccountType { get; set; }
    #endregion

    #region LISTS
    public List<Transaction>? Transactions { get; set; }
    #endregion

    public Account()
    {
    }
}
