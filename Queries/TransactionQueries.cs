using Restap.Helpers;
using System.Linq.Expressions;
using PersonalFinance.Models;

namespace PersonalFinance.Queries; 
public static class TransactionQueries {
    public static IQueryable<Transaction> SearchTransactions(this IQueryable<Transaction> transactions, string searchInfo)
    {
        Expression<Func<Transaction, bool>> Search = transaction => 
            !searchInfo.IsNullOrEmpty() ?
                transaction.Description.ToLower().Contains(searchInfo.ToLower()) ||
                transaction.Amount.ToString().ToLower().Contains(searchInfo.ToLower()) :
            true;

        return transactions.Where(Search);
    }
}
