using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Tasks;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.EasyTranslate.Models.Responses.Tasks;

public class TaskFileResponse(Data<TaskAttributes> dto) : TaskResponse(dto)
{
    [Display("Target file")]
    public FileReference Content { get; set; } = new();
}