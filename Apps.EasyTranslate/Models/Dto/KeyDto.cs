using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto;

public class KeyDto
{
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("text")]
    public string Text { get; set; }
}