using Apps.EasyTranslate.Api;
using Apps.EasyTranslate.Constants;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Tasks;
using Apps.EasyTranslate.Models.Requests;
using Apps.EasyTranslate.Models.Responses.Tasks;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;
using System.Net;

namespace Apps.EasyTranslate.Actions;

[ActionList]
public class TaskActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : AppInvocable(invocationContext)
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

    [Action("Download target content", Description = "Download target content for a task")]
    public async Task<DownloadTargetContentResponse> DownloadTargetContent([ActionParameter] DownloadTargetContentRequest downloadRequest)
    {
        var token = await Client.GetToken(Creds);

        var request = new EasyTranslateRequest(new()
        {
            Url = downloadRequest.TargetContentUrl,
            Method = Method.Get
        }, token);

        var response = await Client.ExecuteRequest(request);

        var bytes = response.RawBytes ?? throw new WebException("No content found");
        var memoryStream = new MemoryStream(bytes);

        string fileName = downloadRequest.FileName ?? $"target-content-{DateTime.Now}.json";
        var fileReference = await fileManagementClient.UploadAsync(memoryStream, ContentType.Json, fileName);
        return new DownloadTargetContentResponse { File = fileReference };
    }
}