using Microsoft.AspNetCore.Components;
using PersonalFinance.Helpers.APIs;

namespace PersonalFinance.Helpers; 
public class General : ComponentBase {
    [Inject] protected HttpClient Http { get; set; }
    [Inject] protected NavigationManager NavigationManager { get; set; }
    [Inject] protected DialogService DialogService { get; set; }

    #region APIs
    [Inject] public AccountsAPI AccountsAPI { get; set; }
    [Inject] public TransactionsAPI TransactionsAPI { get; set; }
    #endregion
}
