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

    public async Task InvalidConfirmation(FormInvalidSubmitEventArgs args)
    {
        await DialogService.Alert(string.Join("<br/>", args.Errors), "Erro");
    }

    public async Task ErrorMsg(Exception ex)
    {
        await DialogService.Alert(ex.InnerException?.Message ?? ex.Message, "Erro");
    }

    public async Task<bool> DialogConfirmation(string message, string title)
    {
        var result = await DialogService.Confirm(
            message, 
            title,
            new ConfirmOptions() {
                OkButtonText = "Sim",
                CancelButtonText = "Não"
            });

        return result ?? false;
    }
}
