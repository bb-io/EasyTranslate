using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Keys;

public class TranslationKeyAttributes
{
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("library_id")]
    public string LibraryId { get; set; }
    
    [JsonProperty("translations")]
    public TranslationDto[] Translations { get; set; }
    
    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }
    
    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; }
}
