using Apps.EasyTranslate.Models.Dto.Generic;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Webhooks;

public class GetWebhooksDto
{
    [JsonProperty("data")]
    public Data<WebhooksAttributes>[] Data { get; set; }
}
