using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Libraries;

public class Overview
{
    [JsonProperty("language_code")]
    public string LanguageCode { get; set; }
    
    [JsonProperty("total_keys")]
    public int TotalKeys { get; set; }
    
    [JsonProperty("total_strings")]
    public int TotalStrings { get; set; }
    
    [JsonProperty("strings_translated")]
    public int StringsTranslated { get; set; }
}