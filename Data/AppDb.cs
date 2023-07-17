using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Models;

namespace PersonalFinance.Data; 
public class AppDb : IdentityDbContext {
    public AppDb(DbContextOptions<AppDb> options)
        : base(options)
    {
    }
    
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Account> Accounts { get; set; }
}

