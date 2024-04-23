using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto;

public class SourceLanguageDto
{
    [JsonProperty("code")]
    public string Code { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
}