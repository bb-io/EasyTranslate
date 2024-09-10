using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Projects;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Responses.Projects;

public class ProjectResponse
{
    public ProjectResponse()
    { }
    
    public ProjectResponse(Data<ProjectAttributes> data)
    {
        Id = data.Id;
        Name = data.Attributes.Name;
        SourceContentUrl = data.Attributes.SourceContent;
        SourceLanguage = data.Attributes.SourceLanguage;
        TargetLanguages = data.Attributes.TargetLanguages.ToList();
        Status = data.Attributes.Status;
        CreatedAt = data.Attributes.CreatedAt;
        UpdatedAt = data.Attributes.UpdatedAt;
        Progress = data.Attributes.Progress.Percent;
        WordsCount = data.Attributes.WordsCount;
        FileName = data.Attributes.FileName;
        Price = data.Attributes.Price.Total;
        WorkflowId = data.Attributes.Workflow.Id;
    }
    
    public ProjectResponse(Data<V1ProjectAttributes> data)
    {
        Id = data.Id;
        Name = data.Attributes.Name;
        SourceContentUrl = data.Attributes.SourceContent;
        SourceLanguage = data.Attributes.SourceLanguage;
        TargetLanguages = data.Attributes.TargetLanguages.ToList();
        Status = data.Attributes.Status;
        CreatedAt = data.Attributes.CreatedAt;
        UpdatedAt = data.Attributes.UpdatedAt;
        Progress = data.Attributes.Progress.Percent;
        WordsCount = data.Attributes.WordsCount;
        FileName = data.Attributes.FileName;
        Price = data.Attributes.Price.Total;
        WorkflowId = data.Attributes.Workflow;
    }

    public string Id { get; set; }
    
    public string Name { get; set; }

    [Display("Source content URL")]
    public string SourceContentUrl { get; set; }

    [Display("Source language")]
    public string SourceLanguage { get; set; }
    
    [Display("Target languages")]
    public List<string> TargetLanguages { get; set; }
    
    public string Status { get; set; }
    
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }
    
    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }

    public long Progress { get; set; }
    
    [Display("Words count")]
    public long WordsCount { get; set; }
    
    [Display("File name")]
    public string FileName { get; set; }

    public long Price { get; set; }

    [Display("Workflow ID")]
    public string WorkflowId { get; set; }
}