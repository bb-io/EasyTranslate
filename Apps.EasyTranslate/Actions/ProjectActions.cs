using System.Net.Http.Headers;
using RestSharp;
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
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Actions;

[ActionList]
public class ProjectActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Get all projects", Description = "Get all projects for a team")]
    public async Task<FetchAllProjectsResponse> FetchAllProjects([ActionParameter] FetchAllProjectsRequest fetchRequest)
    {
        string baseEndpoint = $"{ApiEndpoints.ProjectBase}/teams/[teamname]/projects";

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
        string endpoint = $"{ApiEndpoints.ProjectBase}/teams/[teamname]/projects";

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
        var teamName = Creds.Get(CredsNames.Teamname);
        string endpoint = $"{ApiEndpoints.ProjectBase}/teams/{teamName.Value}/projects";
        string token = await Client.GetToken(Creds);
        var options = new RestClientOptions(Client.BuildUrl(Creds))
        {
            MaxTimeout = -1,
        };
        
        var client = new RestClient(options);
        var easyTranslateRequest = new RestRequest(endpoint, Method.Post);
        easyTranslateRequest.AddHeader("Authorization", $"Bearer {token}");

        easyTranslateRequest.AlwaysMultipartFormData = true;
        easyTranslateRequest.AddParameter("data[type]", "projects");

        int fileIndex = 0;
        foreach (var file in request.Files)
        {
            var stream = await fileManagementClient.DownloadAsync(file);
            var bytes = await stream.GetByteData();
            easyTranslateRequest.AddFile($"data[attributes][files][{fileIndex}]", bytes, file.Name);
            fileIndex++;
        }

        easyTranslateRequest.AddParameter("data[attributes][source_language]", request.SourceLanguage);

        int targetLanguageIndex = 0;
        foreach (var language in request.TargetLanguages)
        {
            easyTranslateRequest.AddParameter($"data[attributes][target_languages][{targetLanguageIndex}]", language);
            targetLanguageIndex++;
        }

        easyTranslateRequest.AddParameter("data[attributes][workflow_id]", request.WorkflowId);

        if (!string.IsNullOrEmpty(request.CallbackUrl))
        {
            easyTranslateRequest.AddParameter("data[attributes][callback_url]", request.CallbackUrl);
        }

        if (!string.IsNullOrEmpty(request.FolderName))
        {
            easyTranslateRequest.AddParameter("data[attributes][folder_name]", request.FolderName);
        }

        if (!string.IsNullOrEmpty(request.FolderId))
        {
            easyTranslateRequest.AddParameter("data[attributes][folder_id]", request.FolderId);
        }

        if (request.Deadline.HasValue)
        {
            easyTranslateRequest.AddParameter("data[attributes][preferred_deadline]",
                request.Deadline.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        var response = await client.ExecuteAsync(easyTranslateRequest);
        if (!response.IsSuccessful)
        {
            throw new Exception($"API call failed: {response.Content}");
        }

        var projectResponse = JsonConvert.DeserializeObject<GetAllProjectsDto>(response.Content);
        return new ProjectResponse(projectResponse.Data.FirstOrDefault() ?? throw new Exception("No project returned"));
    }
}