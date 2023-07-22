using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinance.Models; 
public class Account {    
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "A Descrição precisa ser informada.")]
    public string Name { get; set; }

    public double OpeningBalance { get; set; }

    #region FK
    [ForeignKey(nameof(BankId))]
    [Range(1, int.MaxValue, ErrorMessage = "Um Banco deve ser selecionado.")]
    public int BankId { get; set; }

    public Bank? Bank { get; set; }
    #endregion

    public Account()
    {
    }
}
