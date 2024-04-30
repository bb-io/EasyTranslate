using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Languages;

public class TargetLanguageDto
{
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("code")]
    public string Code { get; set; }
}