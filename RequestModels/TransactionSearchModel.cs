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

    public TransactionSearchModel()
    {
        StartDate = DateTime.Now;
        EndDate = DateTime.Now;
    }

    public TransactionStatus? Status { get; set; }

    public string GenerateQueryString()
    {
        var queryBuilder = new QueryBuilder();

        if (!string.IsNullOrEmpty(searchInfo)) {
            queryBuilder.Add(nameof(searchInfo), searchInfo);
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

        return queryBuilder.ToQueryString().ToString();
    }
}
