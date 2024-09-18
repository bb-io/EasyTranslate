using Apps.EasyTranslate.Models.Dto;
using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Tasks;
using Apps.EasyTranslate.Models.Responses.Tasks;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Responses.Projects;

public class ProjectV1Response : ProjectResponse
{
    public ProjectV1Response(DataDto<Data<V1ProjectAttributes>, TaskAttributes> data)
    {
        Id = data.Data.Id;
        Name = data.Data.Attributes.Name;
        SourceContentUrl = data.Data.Attributes.SourceContent;
        SourceLanguage = data.Data.Attributes.SourceLanguage;
        TargetLanguages = data.Data.Attributes.TargetLanguages.ToList();
        Status = data.Data.Attributes.Status;
        CreatedAt = data.Data.Attributes.CreatedAt;
        UpdatedAt = data.Data.Attributes.UpdatedAt;
        Progress = data.Data.Attributes.Progress.Percent;
        WordsCount = data.Data.Attributes.WordsCount;
        FileName = data.Data.Attributes.FileName;
        Price = data.Data.Attributes.Price.Total;
        WorkflowId = data.Data.Attributes.Workflow;
        Tasks = data.Included?.Select(x => new TaskResponse(x)).ToList() ?? new();
    }
    
    [Display("Tasks")]
    public List<TaskResponse> Tasks { get; set; } 
}