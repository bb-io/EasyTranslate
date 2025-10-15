using Apps.EasyTranslate.Constants;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Folders;
using Apps.EasyTranslate.Models.Requests;
using Apps.EasyTranslate.Models.Responses.Folders;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.EasyTranslate.Actions;

[ActionList("Folders")]
public class FolderActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get all folders", Description = "Get all folders for a team")]
    public async Task<GetAllFoldersResponse> GetAllFolders()
    {
        string endpoint = $"{ApiEndpoints.TeamBaseV1}/[teamname]/folders";
        var dto = await Client.ExecuteWithJson<GetAllFoldersDto>(endpoint, Method.Get, null, Creds);
        return new GetAllFoldersResponse(dto);
    }
    
    [Action("Get folder", Description = "Get a folder for a team")]
    public async Task<FolderResponse> GetFolder([ActionParameter] FolderRequest request)
    {
        string endpoint = $"{ApiEndpoints.TeamBaseV1}/[teamname]/folders/{request.FolderId}";
        var dto = await Client.ExecuteWithJson<GetFolderDto>(endpoint, Method.Get, null, Creds);
        return new FolderResponse(dto.Data);
    }
    
    [Action("Create folder", Description = "Create a folder for a team")]
    public async Task<FolderResponse> CreateFolder([ActionParameter] CreateFolderRequest request)
    {
        string endpoint = $"{ApiEndpoints.TeamBaseV1}/[teamname]/folders";
        var body = new
        {
            data = new
            {
                type = "project-folder",
                attributes = new
                {
                    name = request.Name
                }
            }
        };
        
        var dto = await Client.ExecuteWithJson<GetFolderDto>(endpoint, Method.Post, body, Creds);
        return new FolderResponse(dto.Data);
    }
    
    [Action("Update folder", Description = "Update a folder for a team")]
    public async Task<FolderResponse> UpdateFolder([ActionParameter] FolderRequest request, 
        [ActionParameter] UpdateFolderRequest updateRequest)
    {
        string endpoint = $"{ApiEndpoints.TeamBaseV1}/[teamname]/folders/{request.FolderId}";

        var attributes = new Dictionary<string, object>
        {
            { "name", updateRequest.Name }
        };

        if (updateRequest.IncludedProjectIds != null)
        {
            attributes.Add("included_project_ids", updateRequest.IncludedProjectIds);
        }
        if (updateRequest.ExcludedProjectIds != null)
        {
            attributes.Add("excluded_project_ids", updateRequest.ExcludedProjectIds);
        }

        var body = new
        {
            data = new
            {
                type = "project-folder",
                attributes
            }
        };
    
        var dto = await Client.ExecuteWithJson<GetFolderDto>(endpoint, Method.Patch, body, Creds);
        return new FolderResponse(dto.Data);
    }
}