using Apps.EasyTranslate.Api;
using Apps.EasyTranslate.Constants;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Projects;
using Apps.EasyTranslate.Models.Requests;
using Apps.EasyTranslate.Models.Responses.Projects;
using Apps.EasyTranslate.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.EasyTranslate.Actions;

[ActionList]
public class ProjectActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Fetch all projects", Description = "Fetch all projects for a team")]
    public async Task<FetchAllProjectsResponse> FetchAllProjects([ActionParameter] TeamRequest request,
        [ActionParameter] FetchAllProjectsRequest fetchRequest)
    {
        string baseEndpoint = $"{ApiEndpoints.ProjectBase}/teams/{request.TeamName}/projects";

        var allProjects = new List<Data<ProjectAttributes>>();
        int currentPage = 1;
        MetaPagination meta;

        do
        {
            string endpoint = QueryBuilder.BuildProjectsEndpoint(baseEndpoint, fetchRequest);
            var dto = await Client.ExecuteWithJson<GetAllProjectsDto>(endpoint, Method.Get, null, Creds);

            if (dto?.Data != null)
            {
                allProjects.AddRange(dto.Data);
                meta = dto.Meta;
                currentPage++;
            }
            else
            {
                break;
            }
        } while (currentPage <= (meta?.LastPage ?? 1));

        return new FetchAllProjectsResponse(allProjects);
    }
    
    [Action("Create a project from JSON content", Description = "Create a project from JSON content")]
    public async Task<ProjectResponse>  CreateProjectFromJson([ActionParameter] CreateProjectFromJsonRequest request)
    {
        string endpoint = $"{ApiEndpoints.ProjectBase}/teams/{request.TeamName}/projects";

        var dto = new
        {
            data = new
            {
                type = "projects",
                attributes = new
                {
                    source_language = request.SourceLanguage,
                    target_languages = request.TargetLanguages,
                    workflow_id = request.WorkflowId,
                    content = new
                    {
                        key = request.Content
                    }
                }
            }
        };
        
        var response = await Client.ExecuteWithJson<ProjectResponse>(endpoint, Method.Post, dto, Creds);
        return response;
    }
    
    [Action("Create a project from a file", Description = "Create a project from a file")]
    public async Task<ProjectResponse> CreateProjectFromFile([ActionParameter] CreateProjectFromFileRequest request)
    {
        string endpoint = $"{ApiEndpoints.ProjectBase}/teams/{request.TeamName}/projects";

        string token = await Client.GetToken(Creds);
        string baseUrl = Client.BuildUrl(Creds);

        var easyTranslateRequest = new EasyTranslateRequest(new EasyTranslateRequestParameters
        {
            Url = baseUrl + endpoint,
            Method = Method.Post
        }, token);

        var formData = new MultipartFormDataContent();

        int fileIndex = 0;
        foreach (var file in request.Files)
        {
            var stream = await fileManagementClient.DownloadAsync(file);
            var bytes = await stream.GetByteData();
            formData.Add(new ByteArrayContent(bytes), $"data[attributes][files][{fileIndex++}]");
        }

        formData.Add(new StringContent(request.SourceLanguage), "source_language");

        foreach (var language in request.TargetLanguages)
        {
            formData.Add(new StringContent(language), "target_languages[]");
        }

        if (request.Deadline.HasValue)
        {
            formData.Add(new StringContent(request.Deadline.Value.ToString("o")), "deadline");
        }
        if (!string.IsNullOrWhiteSpace(request.CallbackUrl))
        {
            formData.Add(new StringContent(request.CallbackUrl), "callback_url");
        }
        if (!string.IsNullOrWhiteSpace(request.FolderName))
        {
            formData.Add(new StringContent(request.FolderName), "folder_name");
        }
        if (!string.IsNullOrWhiteSpace(request.FolderId))
        {
            formData.Add(new StringContent(request.FolderId), "folder_id");
        }
        
        formData.Add(new StringContent(request.WorkflowId), "workflow_id");

        foreach (var content in formData)
        {
            easyTranslateRequest.AddParameter(content.Headers.ContentDisposition.Name, content, ParameterType.RequestBody);
        }

        var response = await Client.ExecuteRequest(easyTranslateRequest);
        var projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(response.Content);
        
        return projectResponse;
    }
}