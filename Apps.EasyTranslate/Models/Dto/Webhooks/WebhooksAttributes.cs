using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Webhooks;

public class WebhooksAttributes
{
    [JsonProperty("platform")]
    public string Platform { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("events")]
    public string[] Events { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("disabled_reason")]
    public string DisabledReason { get; set; }

    [JsonProperty("application_id")]
    public string ApplicationId { get; set; }

    [JsonProperty("is_enabled")]
    public bool IsEnabled { get; set; }
}
