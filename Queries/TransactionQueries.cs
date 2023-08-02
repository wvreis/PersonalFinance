using Restap.Helpers;
using System.Linq.Expressions;
using PersonalFinance.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinance.Queries; 
public static class TransactionQueries {
    public static IQueryable<Transaction> SearchTransactions(this IQueryable<Transaction> transactions, string searchInfo)
    {
        Expression<Func<Transaction, bool>> Search = transaction => 
            !searchInfo.IsNullOrEmpty() ?
                transaction.Description.ToLower().Contains(searchInfo.ToLower()) ||
                transaction.Amount.ToString().ToLower().Contains(searchInfo.ToLower()) :
            true;

        Expression<Func<Transaction, bool>> SearchVector = transaction =>
            transaction.SearchVector.Matches(EF.Functions.PhraseToTsQuery(searchInfo));

        var result = transactions.Where(Search);

        if (!result.Any())
            result = transactions.Where(SearchVector);

        return result;
    }
}
