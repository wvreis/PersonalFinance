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
        #endregion
    }
}
