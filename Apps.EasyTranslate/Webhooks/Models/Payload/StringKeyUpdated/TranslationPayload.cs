using Newtonsoft.Json;

namespace Apps.EasyTranslate.Webhooks.Models.Payload.StringKeyUpdated;

public class TranslationPayload
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("key")]
    public string Key { get; set; }
    
    [JsonProperty("text")]
    public string Text { get; set; }
    
    [JsonProperty("status")]
    public string Status { get; set; }
    
    [JsonProperty("segment_id")]
    public int SegmentId { get; set; }
    
    [JsonProperty("language_code")]
    public string LanguageCode { get; set; }
    
    [JsonProperty("library_id")]
    public string LibraryId { get; set; }
    
    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }
    
    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; }
}