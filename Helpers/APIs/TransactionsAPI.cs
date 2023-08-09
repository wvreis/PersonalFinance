using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using PersonalFinance.Controllers;
using PersonalFinance.Helpers.Routes;
using PersonalFinance.Models;
using PersonalFinance.RequestModels;

namespace PersonalFinance.Helpers.APIs; 
public class TransactionsAPI {
    private HttpClient _http { get; set; }
    public TransactionsAPI(HttpClient http)
    {
        _http = http;
    }

    #region GET
    public async Task<Transaction> Get(int id) =>
        await _http.GetFromJsonAsync<Transaction>(ApiRoute.Get(id));

    public async Task<List<Transaction>> GetSearch(TransactionSearchModel? searchModel = null) =>
        await _http.GetFromJsonAsync<List<Transaction>>(ApiRoute.GetSearch(searchModel));

    public async Task<List<Bank>> GetBanks() =>
        await _http.GetFromJsonAsync<List<Bank>>(ApiRoute.GetBanks());

    public async Task<List<Account>> GetAccounts() =>
        await _http.GetFromJsonAsync<List<Account>>(ApiRoute.GetAccounts());

    public async Task<List<TransactionType>> GetAccountTypes() =>
        await _http.GetFromJsonAsync<List<TransactionType>>(ApiRoute.GetTransactionTypeTypes());

    public async Task<Tuple<double, double, double>> GetTotalOutgoingAmountForPeriod(TransactionSearchModel? searchModel = null) =>
        await _http.GetFromJsonAsync<Tuple<double, double, double>>(ApiRoute.GetTotalOutgoingAmountForPeriod(searchModel));

    public async Task<Tuple<double, double, double>> GetTotalIncomingAmountForPeriod(TransactionSearchModel? searchModel = null) =>
        await _http.GetFromJsonAsync<Tuple<double, double, double>>(ApiRoute.GetTotalIncomingAmountForPeriod(searchModel));
    #endregion

    #region POST
    public async Task<HttpResponseMessage> PostTransaction(Transaction transaction) =>
        await _http.PostAsJsonAsync(ApiRoute.PostTransaction(), transaction);
    #endregion

    #region PUT
    public async Task<HttpResponseMessage> PutTransaction(int id, Transaction transaction) =>
        await _http.PutAsJsonAsync(ApiRoute.PutTransaction(id), transaction);

    public async Task<HttpResponseMessage> PutCancelTransaction(int id) =>
        await _http.PutAsync(ApiRoute.PutCancelTransaction(id), null);
    #endregion

    #region DELETE
    public async Task<HttpResponseMessage> DeleteTransaction(int id) => 
        await _http.DeleteAsync(ApiRoute.DeleteTransaction(id));
    #endregion

    public static class ApiRoute
    {
        public const string URL = $"api/{TransactionsRoutes.Index}";

        #region GET
        public static string Get(int id) =>
            $"./{URL}/{nameof(TransactionsController.GetTransaction)}/{id}";

        public static string GetSearch(TransactionSearchModel? searchModel) =>
            $"./{URL}/{nameof(TransactionsController.GetTransactions)}" +            
            searchModel.GenerateQueryString();

        public static string GetBanks() =>
            $"./{URL}/{nameof(TransactionsController.GetBanks)}";

        public static string GetAccounts() =>
            $"./{URL}/{nameof(TransactionsController.GetAccounts)}";

        public static string GetTransactionTypeTypes() =>
            $"./{URL}/{nameof(TransactionsController.GetTransactionTypes)}";

        public static string GetTotalOutgoingAmountForPeriod(TransactionSearchModel? searchModel) =>
            $"./{URL}/{nameof(TransactionsController.GetTotalOutgoingAmountForPeriod)}" +
            searchModel.GenerateQueryString();

        public static string GetTotalIncomingAmountForPeriod(TransactionSearchModel? searchModel) =>
            $"./{URL}/{nameof(TransactionsController.GetTotalIncomingAmountForPeriod)}" +
            searchModel.GenerateQueryString();
        #endregion

        #region POST
        public static string PostTransaction() =>
            $"./{URL}/{nameof(TransactionsController.PostTransaction)}";
        #endregion

        #region PUT
        public static string PutTransaction(int id) =>
            $"./{URL}/{nameof(TransactionsController.PutTransaction)}/{id}";

        public static string PutCancelTransaction(int id) =>
            $"./{URL}/{nameof(TransactionsController.PutCancelTransaction)}/{id}";
        #endregion

        #region DELETE
        public static string DeleteTransaction(int id) =>
            $"./{URL}/{nameof(TransactionsController.DeleteTransaction)}/{id}";
        #endregion
    }
}
