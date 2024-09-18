using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Workflows;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Projects;

public class ProjectWorkflowAttributes : BaseProjectAttributes
{
    [JsonProperty("workflow")]
    public Data<WorkflowAttributes> Workflow { get; set; } = new();
}