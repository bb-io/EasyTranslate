using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Requests;

public class CreateProjectFromJsonRequest : CreateProjectRequest
{
    [Display("Content")]
    public string Content { get; set; }

    [Display("Project name")]
    public string? Name { get; set; }

    [Display("Preferred deadline")]
    public DateTime? PreferredDeadline { get; set; }
}