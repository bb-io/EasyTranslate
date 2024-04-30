using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.TranslationStrings;

public class TranslationStringAttributes
{
    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("key_id")]
    public string KeyId { get; set; }

    [JsonProperty("library_id")]
    public string LibraryId { get; set; }

    [JsonProperty("language_code")]
    public string LanguageCode { get; set; }

    [JsonProperty("history")]
    public List<TranslationStringHistoryDto> History { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
