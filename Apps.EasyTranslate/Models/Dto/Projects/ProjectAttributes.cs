using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Projects;

public class ProjectAttributes : BaseProjectAttributes
{
    [JsonProperty("workflow")]
    public string Workflow { get; set; }
}