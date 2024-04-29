using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Webhooks;

public class GetWebhooksDto
{
    [JsonProperty("data")]
    public List<WebhookDto> Data { get; set; }
}
