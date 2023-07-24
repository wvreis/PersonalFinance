using System.ComponentModel.DataAnnotations;

namespace PersonalFinance.Models;

public class TransactionType {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Description { get; set; }

    #region LISTS
    public List<Transaction>? Transactions { get; set; }
    #endregion
}
