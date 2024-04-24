using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Requests;

public class ProjectRequest : TeamRequest
{
    [Display("Project ID")]
    public string ProjectId { get; set; }
}