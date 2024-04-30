using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto;

public class ProgressDto
{
    [JsonProperty("percent")]
    public long Percent { get; set; }

    [JsonProperty("completed_tasks")]
    public long CompletedTasks { get; set; }
}