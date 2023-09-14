namespace PersonalFinance.Blazor.Helpers.Routes;
public static class AccountsRoutes {
    public const string Index = "accounts";
    public const string Addition = $"{Index}/addition";
    public const string Edition = $"{Index}/edition/{{id:int}}";

    public static string GoToAddition() =>
        $"{Addition}";
    public static string GoToEdition(int id) =>
        $"{Index}/edition/{id}";
    public static string GoToIndex() =>
        $"{Index}";
}
