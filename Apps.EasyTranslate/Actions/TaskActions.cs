using Apps.EasyTranslate.Constants;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Tasks;
using Apps.EasyTranslate.Models.Requests;
using Apps.EasyTranslate.Models.Responses.Tasks;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.EasyTranslate.Actions;

[ActionList]
public class TaskActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get all tasks for a project", Description = "Get all tasks for a project")]
    public async Task<GetAllTasksResponse> GetAllTasks([ActionParameter] ProjectRequest request)
    {
        string endpoint = $"{ApiEndpoints.ProjectBase}/teams/{request.TeamName}/projects/{request.ProjectId}/tasks";
        var dto = await Client.ExecuteWithJson<GetAllTasksDto>(endpoint, Method.Get, null, Creds);
        return new GetAllTasksResponse(dto);
    }
    
    [Action("Get task by ID", Description = "Get task from project by ID")]
    public async Task<TaskResponse> GetTaskById([ActionParameter] TaskRequest request)
    {
        string endpoint = $"{ApiEndpoints.ProjectBase}/teams/{request.TeamName}/projects/{request.ProjectId}/tasks/{request.TaskId}";
        var dto = await Client.ExecuteWithJson<GetTaskDto>(endpoint, Method.Get, null, Creds);
        return new TaskResponse(dto.Data);
    }
}