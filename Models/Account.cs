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
    [ForeignKey(nameof(BankNumber))]
    public int? BankNumber { get; set; }
    public Bank? Bank { get; set; }
    #endregion
}
