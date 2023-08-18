using Microsoft.AspNetCore.Http.Extensions;
using PersonalFinance.Models;
using Restap.Helpers;

namespace PersonalFinance.RequestModels; 
public class TransactionSearchModel {
    string? searchInfo;
    public string? SearchInfo { 
        get => API.UnprocessString(searchInfo); 
        set => searchInfo = value; 
    }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public TransactionStatus? Status { get; set; }

    public int? AccountId { get; set; }

    public TransactionSearchModel()
    {
        DateTime today = DateTime.Now;
        StartDate = new DateTime(today.Year, today.Month, 1);
        EndDate = StartDate.Value.AddMonths(1).AddDays(-1);
    }

    public string GenerateQueryString()
    {
        var queryBuilder = new QueryBuilder();

        if (!string.IsNullOrEmpty(SearchInfo)) {
            queryBuilder.Add(nameof(SearchInfo), SearchInfo);
        }

        if (StartDate.HasValue) {
            queryBuilder.Add(nameof(StartDate), StartDate.Value.ToString("yyyy-MM-dd"));
        }

        if (EndDate.HasValue) {
            queryBuilder.Add(nameof(EndDate), EndDate.Value.ToString("yyyy-MM-dd"));
        }

        if (Status.HasValue) {
            queryBuilder.Add(nameof(Status), Status.Value.ToString());
        }

        if (AccountId.HasValue) {
            queryBuilder.Add(nameof(AccountId), AccountId.Value.ToString());
        }

        return queryBuilder.ToQueryString().ToString();
    }
}
