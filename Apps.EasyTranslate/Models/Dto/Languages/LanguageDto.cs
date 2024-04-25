using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Languages;

public class LanguageDto
{
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("code")]
    public string Code { get; set; }
    
    [JsonProperty("target_languages")]
    public TargetLanguageDto[] TargetLanguages { get; set; }
}