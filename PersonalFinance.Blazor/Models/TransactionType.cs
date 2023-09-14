using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinance.Blazor.Models;

public class TransactionType {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Description { get; set; }

    public TransactionNature Nature { get; set; }

    #region FK
    [ForeignKey(nameof(TransactionTypeGroupId))]
    public int TransactionTypeGroupId { get; set; }
    public TransactionTypeGroup? TransactionTypeGroup { get; set; }
    #endregion

    #region LISTS
    public List<Transaction>? Transactions { get; set; }
    #endregion
}
