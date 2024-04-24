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
    public async Task<ProjectResponse> CreateProjectFromJson([ActionParameter] CreateProjectFromJsonRequest request)
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

        var client = new RestClient(Client.BuildUrl(Creds));
        var easyTranslateRequest = new RestRequest(endpoint, Method.Post);
        easyTranslateRequest.AddHeader("Authorization", $"Bearer {token}");

        easyTranslateRequest.AddParameter("data[type]", "projects", ParameterType.GetOrPost);

        int fileIndex = 0;
        foreach (var file in request.Files)
        {
            var stream = await fileManagementClient.DownloadAsync(file);
            var bytes = await stream.GetByteData();
            easyTranslateRequest.AddFile($"data[attributes][files][{fileIndex}]", bytes, file.Name);
            fileIndex++;
        }

        easyTranslateRequest.AddParameter("data[attributes][source_language]", request.SourceLanguage,
            ParameterType.GetOrPost);

        int targetLanguageIndex = 0;
        foreach (var language in request.TargetLanguages)
        {
            easyTranslateRequest.AddParameter($"data[attributes][target_languages][{targetLanguageIndex}]", language,
                ParameterType.GetOrPost);

            targetLanguageIndex += 1;
        }

        easyTranslateRequest.AddParameter("data[attributes][workflow_id]", request.WorkflowId, ParameterType.GetOrPost);
        
        if (request.CallbackUrl != null)
        {
            easyTranslateRequest.AddParameter("data[attributes][callback_url]", request.CallbackUrl,
                ParameterType.GetOrPost);
        }
        if (request.FolderName != null)
        {
            easyTranslateRequest.AddParameter("data[attributes][folder_name]", request.FolderName,
                ParameterType.GetOrPost);
        }
        if (request.FolderId != null)
        {
            easyTranslateRequest.AddParameter("data[attributes][folder_id]", request.FolderId, ParameterType.GetOrPost);
        }
        if (request.Deadline.HasValue)
        {
            easyTranslateRequest.AddParameter("data[attributes][preferred_deadline]",
                request.Deadline.Value.ToString("o"), ParameterType.GetOrPost);
        }

        var response = await client.ExecuteAsync(easyTranslateRequest);
        if (response.IsSuccessful)
        {
            var projectResponse = JsonConvert.DeserializeObject<GetAllProjectsDto>(response.Content);
            return new ProjectResponse(projectResponse.Data.FirstOrDefault() ?? throw new Exception("No project returned"));
        }
        
        throw new HttpRequestException($"Error {response.StatusCode}: {response.Content}");
    }
}