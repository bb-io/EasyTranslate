using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class CreateProjectRequest : TeamRequest
{
    [Display("Source language")]
    public string SourceLanguage { get; set; }
    
    [Display("Target languages")]
    public IEnumerable<string> TargetLanguages { get; set; }

    [Display("Workflow ID"), DataSource(typeof(WorkflowDataHandler))]
    public string WorkflowId { get; set; }
}
