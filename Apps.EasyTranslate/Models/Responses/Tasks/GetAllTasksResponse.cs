using Apps.EasyTranslate.Models.Dto.Tasks;

namespace Apps.EasyTranslate.Models.Responses.Tasks;

public class GetAllTasksResponse
{
    public List<TaskResponse> Tasks { get; set; }
    
    public GetAllTasksResponse(GetAllTasksDto dto)
    {
        Tasks = dto.Data.Select(x => new TaskResponse(x)).ToList();
    }
}