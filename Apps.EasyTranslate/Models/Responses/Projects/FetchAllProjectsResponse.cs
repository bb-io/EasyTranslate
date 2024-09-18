using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Projects;

namespace Apps.EasyTranslate.Models.Responses.Projects;

public class FetchAllProjectsResponse
{
    public List<ProjectResponse> Projects { get; set; }
    
    public FetchAllProjectsResponse(GetAllProjectsDto dto)
    {
        Projects = dto.Data.Select(x => new ProjectResponse(x)).ToList();
    }
    
    public FetchAllProjectsResponse(List<Data<ProjectWorkflowAttributes>> dataList)
    {
        Projects = dataList.Select(x => new ProjectResponse(x)).ToList();
    }
}