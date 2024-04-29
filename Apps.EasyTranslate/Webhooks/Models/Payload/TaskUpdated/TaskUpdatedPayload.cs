using Apps.EasyTranslate.Models.Dto.Generic;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Webhooks.Models.Payload.TaskUpdated;

public class TaskUpdatedPayload
{
    [JsonProperty("data")]
    public Data<TaskUpdatedAttributes> Data { get; set; }
}
