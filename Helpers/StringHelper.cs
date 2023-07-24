namespace PersonalFinance.Helpers; 
public static class StringHelper {
    public static string GetActiveInactive(this bool status) =>
        status ? "Ativo" : "Inativo";
}
