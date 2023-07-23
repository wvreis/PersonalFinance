using System.ComponentModel.DataAnnotations;

namespace PersonalFinance.Models;

public enum TransactionTypeStatus { Active, Inactive }

public class TransactionType {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Description { get; set; }

    public TransactionTypeStatus Status { get; set; }
}
