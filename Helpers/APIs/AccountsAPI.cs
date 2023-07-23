using PersonalFinance.Controllers;
using PersonalFinance.Helpers.Routes;
using PersonalFinance.Models;
using Restap.Helpers;

namespace PersonalFinance.Helpers.APIs; 
public class AccountsAPI {
    private HttpClient _http { get; set; }
    public AccountsAPI(HttpClient http)
    {
        _http = http;
    }

    #region GET
    public async Task<Account> Get(int id) =>
        await _http.GetFromJsonAsync<Account>(ApiRoute.Get(id));

    public async Task<List<Account>> GetSearch(string searchInfo = null) =>
        await _http.GetFromJsonAsync<List<Account>>(ApiRoute.GetSearch(searchInfo));

    public async Task<List<Bank>> GetBanks() =>
        await _http.GetFromJsonAsync<List<Bank>>(ApiRoute.GetBanks());

    public async Task<List<AccountType>> GerAccountTypes() =>
        await _http.GetFromJsonAsync<List<AccountType>>(ApiRoute.GetAccountTypes());
    #endregion

    #region POST
    public async Task<HttpResponseMessage> PostAccount(Account account) =>
        await _http.PostAsJsonAsync(ApiRoute.PostAccount(), account);
    #endregion

    #region PUT
    public async Task<HttpResponseMessage> PutAccount(int id, Account account) =>
        await _http.PutAsJsonAsync(ApiRoute.PutAccount(id), account);
    #endregion

    #region DELETE
    public async Task<HttpResponseMessage> DeleteAccount(int id) =>
        await _http.DeleteAsync(ApiRoute.DeleteAccount(id));
    #endregion

    public static class ApiRoute
    {
        public const string URL = $"api{AccountsRoutes.Index}";

        #region GET
        public static string Get(int id) =>
            $"./{URL}/{nameof(AccountsController.GetAccount)}/{id}";

        public static string GetSearch(string searchInfo) =>
            $"./{URL}/{nameof(AccountsController.GetAccounts)}?" +
            $"{nameof(searchInfo)}={searchInfo}";

        public static string GetBanks() =>
            $"./{URL}/{nameof(AccountsController.GetBanks)}";

        public static string GetAccountTypes() =>
            $"./{URL}/{nameof(AccountsController.GetAccountTypes)}";
        #endregion

        #region POST
        public static string PostAccount() =>
            $"./{URL}/{nameof(AccountsController.PostAccount)}";
        #endregion

        #region PUT
        public static string PutAccount(int id) =>
            $"./{URL}/{nameof(AccountsController.PutAccount)}/{id}";
        #endregion

        #region DELETE
        public static string DeleteAccount(int id) =>
            $"./{URL}/{nameof(AccountsController.DeleteAccount)}/{id}";
        #endregion
    }
}
