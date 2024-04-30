using Apps.EasyTranslate.Models.Dto.Generic;

namespace Apps.EasyTranslate.Models.Dto.Webhooks;

public class WebhookDto
{
    public Data<WebhooksAttributes> Data { get; set; }
}
