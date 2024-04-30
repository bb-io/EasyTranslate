using Apps.EasyTranslate.Models.Dto.Generic;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Webhooks.Models.Payload.StringKeyUpdated;

public class StringKeyUpdatedPayload
{
    [JsonProperty("data")]
    public Data<StringKeyUpdatedAttributes>[] Data { get; set; }
}