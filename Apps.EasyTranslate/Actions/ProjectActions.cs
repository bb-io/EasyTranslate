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
        var httpClient = new HttpClient();
        var teamName = Creds.Get(CredsNames.Teamname).Value;
        var endpoint = $"{ApiEndpoints.ProjectBase}/teams/{teamName}/projects";
        var token = await Client.GetToken(Creds);

        var requestUrl = Client.BuildUrl(Creds) + endpoint;
        var requestData = new Dictionary<string, string>
        {
            { "data[type]", "project" },
            { "data[attributes][source_language]", request.SourceLanguage }
        };

        var fileIndex = 0;
        foreach (var file in request.Files)
        {
            var stream = await fileManagementClient.DownloadAsync(file);
            var bytes = await stream.GetByteData();
            var fileContent = Convert.ToBase64String(bytes);
            requestData.Add($"data[attributes][files][{fileIndex}]", fileContent);
            fileIndex++;
        }

        var targetLangIndex = 0;
        foreach (var targetLang in request.TargetLanguages)
        {
            requestData.Add($"data[attributes][target_languages][{targetLangIndex}]", targetLang);
            targetLangIndex++;
        }

        if (!string.IsNullOrEmpty(request.CallbackUrl))
        {
            requestData.Add("data[attributes][callback_url]", request.CallbackUrl);
        }

        if (!string.IsNullOrEmpty(request.FolderId))
        {
            requestData.Add("data[attributes][folder_id]", request.FolderId);
        }

        if (!string.IsNullOrEmpty(request.FolderName))
        {
            requestData.Add("data[attributes][folder_name]", request.FolderName);
        }

        if (!string.IsNullOrEmpty(request.WorkflowId))
        {
            requestData.Add("data[attributes][workflow]", request.WorkflowId);
        }

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var content = new FormUrlEncodedContent(requestData);
        var response = await httpClient.PostAsync(requestUrl, content);        
        var responseContent = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<ProjectResponse>(responseContent);
    }
}