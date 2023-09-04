using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersonalFinance.Models;
using System.Linq.Expressions;

namespace PersonalFinance.Queries; 
public static class AccountQueries {
    public static IQueryable<Account> SearchAccounts(this IQueryable<Account> accounts, string searchInfo)
    {
        Expression<Func<Account, bool>> Search = account =>
            !searchInfo.IsNullOrEmpty() ?
                account.Description.ToLower().Contains(searchInfo.ToLower()) || 
                account.Bank.Name.ToLower().Contains(searchInfo.ToLower()) :
            true;

        var result = accounts
            .Include(acc => acc.Bank)
            .Where(Search);

        return result;
    }

    public static IQueryable<Account> WhereStatus(this  IQueryable<Account> accounts, bool status)
    {
        var result = accounts.Where(acc => acc.Status == status);
        return result;
    }
}
