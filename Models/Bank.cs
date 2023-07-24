using System.ComponentModel.DataAnnotations;

namespace PersonalFinance.Models; 
public class Bank {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Range(1, int.MaxValue)]
    public int Number { get; set; }

    #region LISTS
    public List<Account>? Accounts { get; set; }
    #endregion

    public Bank()
    {
            
    }
}