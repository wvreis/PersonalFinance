using Restap.Helpers;
using System.Linq.Expressions;
using PersonalFinance.Blazor.Models;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Blazor.RequestModels;

namespace PersonalFinance.Blazor.Queries; 
public static class TransactionQueries {
    public static IQueryable<Transaction> SearchTransactions(this IQueryable<Transaction> transactions, string searchInfo, string searchVectorInfo = null)
    {
        if (searchInfo.IsNullOrEmpty()) {
            return transactions;
        }

        Expression<Func<Transaction, bool>> SearchByDescriptionAndAmount = transaction =>
            transaction.Description.ToLower().Contains(searchInfo.ToLower()) ||
            transaction.Amount.ToString().ToLower().Contains(searchInfo.ToLower());

        var result = transactions.Where(SearchByDescriptionAndAmount);        

        if (!result.Any()) {
            Expression<Func<Transaction, bool>> SearchVector = transaction =>
                transaction.SearchVector.Matches(EF.Functions.WebSearchToTsQuery(searchVectorInfo));

            result = transactions.Where(SearchVector);
        }

        return result;
    }

    public static IQueryable<Transaction> WherePeriod(this IQueryable<Transaction> transactions, DateTime startDate, DateTime endDate)
    {
        var result = transactions
            .Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date);

        return result;
    }

    public static IQueryable<Transaction> WhereStatus(this IQueryable<Transaction> transactions, TransactionStatus? status) 
    {
        if (status.HasValue) {
            var result = transactions.Where(t => t.Status == status);
            return result;
        }

        return transactions;
    }

    public static IQueryable<Transaction> WhereTransactionTypeNature(this IQueryable<Transaction> transactions, TransactionNature? transactionNature = null)
    {
        if (transactionNature.HasValue) {
            var result = transactions
                .Include(x => x.TransactionType)
                .Where(t => t.TransactionType.Nature == transactionNature.Value);

            return result;
        }

        return transactions;
    }

    public static IQueryable<Transaction> WhereNature(this IQueryable<Transaction> transactions, TransactionNature? transactionNature = null)
    {
        if (transactionNature.HasValue) {
            var result = transactions
                .Where(t => t.Nature == transactionNature.Value);

            return result;
        }

        return transactions;
    }

    public static IQueryable<Transaction> WhereAccount(this IQueryable<Transaction> transactions, int? accountId)
    {
        if (accountId.HasValue) {
            var result = transactions
                .Where(t => t.AccountId == accountId.Value);

            return result;
        }

        return transactions;
    }

    public static IQueryable<Transaction> WhereDefaultFilters (
        this IQueryable<Transaction> transactions, 
        TransactionSearchModel searchModel,         
        string searchVectorInfo = null,
        TransactionNature? transactionNature = null)
    {
        var result = transactions
            .Include(x => x.TransactionType)
            .Include(x => x.Account)
            .WherePeriod(searchModel.StartDate.Value, searchModel.EndDate.Value)
            .WhereTransactionTypeNature(transactionNature)
            .WhereAccount(searchModel.AccountId)
            .SearchTransactions(searchModel.SearchInfo, searchVectorInfo)
            .WhereStatus(searchModel.Status)
            .OrderByDescending(x => x.Date);

        return result;
    }
}
