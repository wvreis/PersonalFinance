using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    }

    public DbSet<Bank> Banks { get; set; }
    public DbSet<Account> Accounts { get; set; }
}

