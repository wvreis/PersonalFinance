using Newtonsoft.Json;

namespace PersonalFinance.Helpers;
public class ValidationError {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Field { get; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int Code { get; set; }
    public string Message { get; }
    public ValidationError(string field, int code, string message)
    {
        Field = field != string.Empty ? field : null;
        Code = code != 0 ? code : 55;
        Message = message;
    }
}
