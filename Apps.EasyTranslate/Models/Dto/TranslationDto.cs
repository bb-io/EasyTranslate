using Apps.EasyTranslate.Models.Dto.Keys;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto;

public class TranslationDto
{
    [JsonProperty("type")]
    public string Type { get; set; }
    
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("attributes")]
    public TranslationAttributes Attributes { get; set; }
}
