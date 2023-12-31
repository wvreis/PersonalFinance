﻿using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinance.Models; 

public enum TransactionStatus { Pending, Completed, Canceled }
public enum TransactionNature { Inbound, Outbound };

public class Transaction {
    [Key]
    public int Id { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O Valor deve ser informado.")]
    public double Amount { get; set; }

    [Range(typeof(DateTime), "01/01/2000", "01/01/2500", ErrorMessage = "Uma Data posterior a {0} deve ser informada.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Descrição deve ser informada.")]
    public string Description { get; set; }

    public TransactionStatus Status { get; set; }

    TransactionNature nature;
    public TransactionNature Nature { 
        get => nature;
        set {
            TransactionType = null;
            nature = value;
        }
    }

    public NpgsqlTsVector? SearchVector { get; }

    #region FK
    [ForeignKey(nameof(AccountId))]
    [Range(1, int.MaxValue, ErrorMessage = "Uma Conta deve ser informada.")]
    public int AccountId { get; set; }
    public Account? Account { get; set; }

    [ForeignKey(nameof(TransactionTypeId))]
    [Range(1, int.MaxValue, ErrorMessage = "Um Tipo de Movimentação deve ser informado.")]
    public int TransactionTypeId { get; set; }
    public TransactionType? TransactionType { get; set; }
    #endregion

    public static Dictionary<TransactionStatus, (string text, int style )> StatusDictionary =>
        new() {
            {TransactionStatus.Pending, new("Pendente", (int)BadgeStyle.Warning) },
            {TransactionStatus.Completed, new("Paga", (int)BadgeStyle.Success) },
            {TransactionStatus.Canceled, new("Cancelada", (int)BadgeStyle.Light) }
        };

    public static Dictionary<TransactionNature, string> NatureDictionary =>
        new() {
            { TransactionNature.Inbound, "Entradas" },
            { TransactionNature.Outbound, "Saídas" }
        };

    public Transaction()
    {
        Date = DateTime.Now;
        Nature = TransactionNature.Outbound;
    }
}
