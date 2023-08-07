namespace PersonalFinance.Services;
public class SpellCheckerService {
    private IWebHostEnvironment _webHostEnvironment;
    static int initialCapacity = 82765;
    static int maxEditDistanceDictionary = 2;
    public SymSpell SymSpell = new SymSpell(initialCapacity, maxEditDistanceDictionary);

    public SpellCheckerService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;

        //load dictionary
        string baseDirectory = _webHostEnvironment.WebRootPath;
        string dictionaryPath = baseDirectory + "\\Dictionaries\\pt_br_50k.txt";
        int termIndex = 0; //column of the term in the dictionary text file
        int countIndex = 1; //column of the term frequency in the dictionary text file

        SymSpell.LoadDictionary(dictionaryPath, termIndex, countIndex);
    }

    public string GetSpellCheckedSearchVectorString(string? searchInfo)
    {
        return SymSpell
            .LookupCompound(searchInfo ?? string.Empty)
            .Select(x => x.term)
            .Single();
    }
}    
