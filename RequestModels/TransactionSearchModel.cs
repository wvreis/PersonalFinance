using Microsoft.AspNetCore.Http.Extensions;
using Restap.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        return queryBuilder.ToQueryString().ToString();
    }
}
