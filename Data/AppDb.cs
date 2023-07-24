﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Models;

namespace PersonalFinance.Data; 
public class AppDb : IdentityDbContext {
    public AppDb(DbContextOptions<AppDb> options)
        : base(options)
    {
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
    }

    public DbSet<Bank> Banks { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountType> AccountTypes { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
}

