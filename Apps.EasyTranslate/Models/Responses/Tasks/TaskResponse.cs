using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Tasks;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Responses.Tasks;

public class TaskResponse
{
    public TaskResponse(Data<TaskAttributes> dto)
    {
        TaskId = dto.Id;
        SourceLanguage = dto.Attributes.SourceLanguage;
        TargetLanguage = dto.Attributes.TargetLanguage;
        Type = dto.Attributes.Type;
        FileName = dto.Attributes.FileName;
        WordCount = dto.Attributes.WordCount;
        Status = dto.Attributes.Status;
        Price = dto.Attributes.Price.Total;
        Progress = dto.Attributes.Progress;
        IsRated = dto.Attributes.IsRated;
        IsContent = dto.Attributes.IsContent;
        RevisionStatus = dto.Attributes.RevisionStatus;
        TargetContentUrl = dto.Attributes.TargetContent;
        CreatedAt = DateTime.Parse(dto.Attributes.CreatedAt);
        UpdatedAt = DateTime.Parse(dto.Attributes.UpdatedAt);
    }

    [Display("Target content URL")]
    public string TargetContentUrl { get; set; }
    
    [Display("Task ID")]
    public string TaskId { get; set; }

    [Display("Source language")]
    public string SourceLanguage { get; set; }
    
    [Display("Target language")]
    public string TargetLanguage { get; set; }
    
    [Display("Type")]
    public string Type { get; set; }
    
    [Display("File name")]
    public string FileName { get; set; }
    
    [Display("Word count")]
    public long WordCount { get; set; }
    
    [Display("Status")]
    public string Status { get; set; }
    
    [Display("Price")]
    public long Price { get; set; }

    public string Currency { get; set; }
    
    [Display("Progress")]
    public string Progress { get; set; }
    
    [Display("Is rated")]
    public bool IsRated { get; set; }
    
    [Display("Is content")]
    public bool IsContent { get; set; }
    
    [Display("Revision status")]
    public string RevisionStatus { get; set; }
    
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }
    
    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }
}