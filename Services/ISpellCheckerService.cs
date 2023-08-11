namespace PersonalFinance.Services; 
public interface ISpellCheckerService {
    string GetSpellCheckedSearchVectorString(string? searchInfo);
}
