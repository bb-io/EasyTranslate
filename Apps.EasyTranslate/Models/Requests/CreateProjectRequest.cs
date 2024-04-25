using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class CreateProjectRequest : TeamRequest
{
    [Display("Source language"), DataSource(typeof(SourceLanguageDataHandler))]
    public string SourceLanguage { get; set; }
    
    [Display("Target languages"), DataSource(typeof(TargetLanguageDataHandler))]
    public IEnumerable<string> TargetLanguages { get; set; }

    [Display("Workflow ID"), DataSource(typeof(WorkflowDataHandler))]
    public string WorkflowId { get; set; }
}
