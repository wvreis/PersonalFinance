using Microsoft.AspNetCore.Components;
using PersonalFinance.Helpers.APIs;

namespace PersonalFinance.Helpers; 
public class General : ComponentBase {
    [Inject] protected HttpClient Http { get; set; }

    #region APIs
    [Inject] public AccountsAPI AccountsAPI { get; set; }
    #endregion
}
