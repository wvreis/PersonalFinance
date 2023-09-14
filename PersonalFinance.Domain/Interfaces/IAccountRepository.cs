using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Domain.Interfaces;

public interface IAccountRepository : IBaseRepository<Account>
{
    Task<List<Account>> GetAccountsSearch(string searchInfo, CancellationToken cancellationToken);
}