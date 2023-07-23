﻿using System.ComponentModel.DataAnnotations;

namespace PersonalFinance.Models; 

public enum AccountTypeStatus { Active, Inactive }

public class AccountType {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Description { get; set; }

    public AccountTypeStatus Status { get; set; }
}
