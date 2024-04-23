using Apps.EasyTranslate.Models.Dto.Generic;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Projects;

public class GetAllProjectsDto
{
    [JsonProperty("data")]
    public Data<ProjectAttributes>[] Data { get; set; }
    
    [JsonProperty("meta")]
    public MetaPagination Meta { get; set; }
}
