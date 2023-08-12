using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Models;

namespace PersonalFinance.Data; 
public class AppDb : IdentityDbContext {
    public AppDb(DbContextOptions<AppDb> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region BANK
        builder.Entity<Bank>()
            .HasMany(x => x.Accounts)
            .WithOne(x => x.Bank)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region ACCOUNTTYPES
        builder.Entity<AccountType>()
            .HasMany(x => x.Accounts)
            .WithOne(x => x.AccountType) 
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region ACCOUNTS
        builder.Entity<Account>()
            .HasMany(x => x.Transactions)
            .WithOne(x => x.Account)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region TRANSACTIONTYPES
        builder.Entity<TransactionType>()
            .HasMany(x => x.Transactions)
            .WithOne(x => x.TransactionType)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region TRANSACTIONTYPEGROUPS
        builder.Entity<TransactionTypeGroup>()
            .HasMany(x => x.TransactionTypes)
            .WithOne(x => x.TransactionTypeGroup) 
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region TRANSACTIONS
        builder.Entity<Transaction>(b => {
            if (Database.IsNpgsql()) {
                b.HasGeneratedTsVectorColumn(
                    p => p.SearchVector,
                    "portuguese",
                    p => new { p.Description })
                    .HasIndex(p => p.SearchVector)
                    .HasMethod("GIN");
            }
        });
        #endregion
    }

    public DbSet<Bank> Banks { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountType> AccountTypes { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<TransactionTypeGroup> TransactionTypeGroups { get; set; }
}

