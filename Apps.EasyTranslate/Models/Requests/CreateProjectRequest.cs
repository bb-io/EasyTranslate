using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Requests;

public class CreateProjectRequest : TeamRequest
{
    [Display("Source language")]
    public string SourceLanguage { get; set; }
    
    [Display("Target languages")]
    public IEnumerable<string> TargetLanguages { get; set; }

    [Display("Workflow ID")]
    public string WorkflowId { get; set; }
}