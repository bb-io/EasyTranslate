using Newtonsoft.Json;

namespace Apps.EasyTranslate.Webhooks.Models.Payload.StringKeyUpdated;

public class StringKeyUpdatedAttributes
{
    [JsonProperty("name")] 
    public string Name { get; set; }
    
    [JsonProperty("library_id")]
    public string LibraryId { get; set; }
    
    [JsonProperty("translations")]
    public List<TranslationPayload> Translations { get; set; }
}