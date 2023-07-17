using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Data;

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

            var cadastrados = _context.Banks.ToList();

            if (!cadastrados.Any()) {
                await _context.Banks.AddRangeAsync(MainBanks.GetBanks());
                await _context.SaveChangesAsync();
            }
        }
    }
}
