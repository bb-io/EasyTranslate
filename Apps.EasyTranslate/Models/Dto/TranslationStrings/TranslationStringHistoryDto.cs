using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.TranslationStrings;

public class TranslationStringHistoryDto
{
    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("agent")]
    public string Agent { get; set; }

    [JsonProperty("user_name")]
    public string UserName { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
