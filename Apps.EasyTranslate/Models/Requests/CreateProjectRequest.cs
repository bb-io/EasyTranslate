namespace Apps.EasyTranslate.Models.Requests;

public class CreateProjectRequest : TeamRequest
{
    public string SourceLanguage { get; set; }
    
    public IEnumerable<string> TargetLanguages { get; set; }

    public string WorkflowId { get; set; }
}