using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinance.Models; 
public class Account {    
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public double OpeningBalance { get; set; }

    #region FK
    [ForeignKey(nameof(BankId))]
    public int BankId { get; set; }
    public Bank Bank { get; set; }
    #endregion
}
