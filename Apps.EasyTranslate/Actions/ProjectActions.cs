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
using RestSharp;

namespace Apps.EasyTranslate.Actions;

[ActionList]
public class ProjectActions : AppInvocable
{
    public ProjectActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    [Action("Fetch all projects")]
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
}