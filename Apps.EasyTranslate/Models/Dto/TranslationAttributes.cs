using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto;

public class TranslationAttributes
{
    [JsonProperty("text")]
    public string Text { get; set; }
    
    [JsonProperty("status")]
    public string Status { get; set; }
    
    [JsonProperty("language_code")]
    public string LanguageCode { get; set; }
    
    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }
    
    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; }
}