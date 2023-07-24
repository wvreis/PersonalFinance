using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Data;
using PersonalFinance.Data.AutoRegisterLists;

namespace PersonalFinance.Services;
public class UpdataDbService : ControllerBase, IHostedService {
    readonly IServiceScopeFactory _scopeFactory;

    public UpdataDbService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(InitTasks, cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => null;

    public async Task InitTasks()
    {
        try {
            UpdateDatabase();
            await AddBanks();
            await AddAcocuntTypes();
        }
        catch (Exception ex) {

            throw ex;
        }
    }

    void UpdateDatabase()
    {
        using (var scope = _scopeFactory.CreateScope()) {
            var db = scope.ServiceProvider.GetRequiredService<AppDb>();
            db.Database.Migrate();
        }
    }

    async Task AddBanks()
    {
        using (var scope = _scopeFactory.CreateScope()) {
            var _context = scope.ServiceProvider.GetRequiredService<AppDb>();

            var alreadyRegistered = _context.Banks.ToList();

            if (!alreadyRegistered.Any()) {
                await _context.Banks.AddRangeAsync(Banks.GetAll());
                await _context.SaveChangesAsync();
            }
        }
    }

    async Task AddAcocuntTypes()
    {
        using (var scope = _scopeFactory.CreateScope()) {
            var _context = scope.ServiceProvider.GetRequiredService<AppDb>();

            var alreadyRegistered = _context.AccountTypes.ToList();

            if (!alreadyRegistered.Any()) {
                await _context.AccountTypes.AddRangeAsync(AccountTypes.GetAll());  
                await _context.SaveChangesAsync();
            }
        }
    }
}
