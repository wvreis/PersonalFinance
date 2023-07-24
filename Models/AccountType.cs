using System.ComponentModel.DataAnnotations;

namespace PersonalFinance.Models; 

public class AccountType {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Description { get; set; }

    public bool Status { get; set; }

    #region LISTS
    public List<Account>? Accounts { get; set; }
    #endregion
}
