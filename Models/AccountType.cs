using System.ComponentModel.DataAnnotations;

namespace PersonalFinance.Models; 
public class AccountType {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Description { get; set; }
}
