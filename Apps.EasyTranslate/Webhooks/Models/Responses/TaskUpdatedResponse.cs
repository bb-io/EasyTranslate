using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Webhooks.Models.Payload.TaskUpdated;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Webhooks.Models.Responses;

public class TaskUpdatedResponse
{
    [Display("String key ID")]
    public string Id { get; set; }

    [Display("Target content URL")]
    public string TargetContentUrl { get; set; }

    [Display("Source language")]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    public string TargetLanguage { get; set; }

    public string Type { get; set; }

    [Display("Project ID")]
    public string ProjectId { get; set; }

    [Display("Supplier ID")]
    public string SupplierId { get; set; }

    [Display("File name")]
    public string FileName { get; set; }

    [Display("Word count")]
    public int WordCount { get; set; }

    public string Status { get; set; }

    [Display("Revision status")]
    public string RevisionStatus { get; set; }

    public TaskUpdatedResponse(Data<TaskUpdatedAttributes> data)
    {
        Id = data.Id;
        TargetContentUrl = data.Attributes.TargetContent;
        SourceLanguage = data.Attributes.SourceLanguage;
        TargetLanguage = data.Attributes.TargetLanguage;
        Type = data.Attributes.Type;
        FileName = data.Attributes.FileName;
        WordCount = data.Attributes.WordCount;
        Status = data.Attributes.Status;
        SupplierId = data.Attributes.SupplierId;
        ProjectId = data.Attributes.Project.Id;
        RevisionStatus = data.Attributes.RevisionStatus;
    }
}
