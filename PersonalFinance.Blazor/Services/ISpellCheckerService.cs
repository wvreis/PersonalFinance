namespace PersonalFinance.Blazor.Services; 
public interface ISpellCheckerService {
    string GetSpellCheckedSearchVectorString(string? searchInfo);
}
