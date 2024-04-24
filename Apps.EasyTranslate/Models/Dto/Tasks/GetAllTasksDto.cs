using Apps.EasyTranslate.Models.Dto.Generic;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Tasks;

public class GetAllTasksDto
{
    [JsonProperty("data")]
    public Data<TaskAttributes>[] Data { get; set; }

    [JsonProperty("meta")]
    public Meta Meta { get; set; }
}