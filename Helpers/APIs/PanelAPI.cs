using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using PersonalFinance.Controllers;
using PersonalFinance.DTOs;
using PersonalFinance.Helpers.Routes;

namespace PersonalFinance.Helpers.APIs; 
public class PanelAPI {
    private HttpClient _http;

    public PanelAPI(HttpClient httpClient)
    {
        _http = httpClient;
    }

    #region GET
    public async Task<PanelDTO> GetPanelItems() =>
        await _http.GetFromJsonAsync<PanelDTO>(ApiRoute.GetPanelItems());

    #endregion

    public static class ApiRoute
    {
        public const string URL = $"api/{PanelRoutes.Index}";

        #region GET
        public static string GetPanelItems() =>
            $"./{URL}/{nameof(PanelController.GetPanelItems)}";
        #endregion
    }
}
