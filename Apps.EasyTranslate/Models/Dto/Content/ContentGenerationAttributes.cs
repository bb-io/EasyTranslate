using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Content;

public class ContentGenerationAttributes
{
    [JsonProperty("generated_content")]
    public string GeneratedContent { get; set; }

    [JsonProperty("finish_reason")]
    public string FinishReason { get; set; }
}