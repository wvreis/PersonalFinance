using PersonalFinance.Controllers;
using PersonalFinance.Helpers.Routes;
using PersonalFinance.Models;

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

    public async Task<List<Transaction>> GetSearch(string searchInfo = null) =>
        await _http.GetFromJsonAsync<List<Transaction>>(ApiRoute.GetSearch(searchInfo));

    public async Task<List<Bank>> GetBanks() =>
        await _http.GetFromJsonAsync<List<Bank>>(ApiRoute.GetBanks());

    public async Task<List<AccountType>> GerAccountTypes() =>
        await _http.GetFromJsonAsync<List<AccountType>>(ApiRoute.GetAccountTypes());
    #endregion

    #region POST
    public async Task<HttpResponseMessage> PostTransaction(Transaction transaction) =>
        await _http.PostAsJsonAsync(ApiRoute.PostTransaction(), transaction);
    #endregion

    #region PUT
    public async Task<HttpResponseMessage> PutTransaction(int id, Transaction transaction) =>
        await _http.PutAsJsonAsync(ApiRoute.PutTransaction(id), transaction);
    #endregion

    #region DELETE
    public async Task<HttpResponseMessage> DeleteTransaction(int id) => 
        await _http.DeleteAsync(ApiRoute.DeleteTransaction(id));
    #endregion

    public static class ApiRoute
    {
        public const string URL = $"api{AccountsRoutes.Index}";

        #region GET
        public static string Get(int id) =>
            $"./{URL}/{nameof(TransactionsController.GetTransaction)}/{id}";

        public static string GetSearch(string searchInfo) =>
            $"./{URL}/{nameof(TransactionsController.GetTransactions)}?" +
            $"{nameof(searchInfo)}={searchInfo}";

        public static string GetBanks() =>
            $"./{URL}/{nameof(TransactionsController.GetBanks)}";

        public static string GetAccountTypes() =>
            $"./{URL}/{nameof(TransactionsController.GetAccountTypes)}";
        #endregion

        #region POST
        public static string PostTransaction() =>
            $"./{URL}/{nameof(TransactionsController.PostTransaction)}";
        #endregion

        #region PUT
        public static string PutTransaction(int id) =>
            $"./{URL}/{nameof(TransactionsController.PutTransaction)}/{id}";
        #endregion

        #region DELETE
        public static string DeleteTransaction(int id) =>
            $"./{URL}/{nameof(TransactionsController.DeleteTransaction)}/{id}";
        #endregion
    }
}
