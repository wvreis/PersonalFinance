using System.ComponentModel.DataAnnotations;

namespace PersonalFinance.Blazor.Models; 
public class TransactionTypeGroup {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Description { get; set; }

    #region LISTS
    public List<TransactionType>? TransactionTypes { get; set; }
    #endregion
}
