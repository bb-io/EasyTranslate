using Apps.EasyTranslate.Models.Dto.Generic;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Projects;

public class ProjectDto
{
    [JsonProperty("data")]
    public Data<ProjectAttributes> Data { get; set; }
}
