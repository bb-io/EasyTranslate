using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Webhooks.Models.Payload.TaskUpdated;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.EasyTranslate.Webhooks.Models.Responses;

public class StringKeyResponse
{
    [Display("String key ID")]
    public string Id { get; set; }

    [Display("Target content")]
    public FileReference TargetContent { get; set; }

    [Display("Source language")]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    public string TargetLanguage { get; set; }

    public string Type { get; set; }

    [Display("File name")]
    public string FileName { get; set; }

    [Display("Word count")]
    public int WordCount { get; set; }

    public string Status { get; set; }

    [Display("Revision status")]
    public string RevisionStatus { get; set; }

    public StringKeyResponse(Data<TaskUpdatedAttributes> data)
    {
        Id = data.Id;
        SourceLanguage = data.Attributes.SourceLanguage;
        TargetLanguage = data.Attributes.TargetLanguage;
        Type = data.Attributes.Type;
        FileName = data.Attributes.FileName;
        WordCount = data.Attributes.WordCount;
        Status = data.Attributes.Status;
        RevisionStatus = data.Attributes.RevisionStatus;
    }
}
