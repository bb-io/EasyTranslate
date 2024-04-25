using Apps.EasyTranslate.Actions;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.EasyTranslate.DataSourceHandlers;

public class ProjectDataHandler(InvocationContext invocationContext, [ActionParameter] ProjectRequest request)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.TeamName))
        {
            throw new InvalidOperationException("You should input a team name first");
        }
        
        var projectActions = new ProjectActions(InvocationContext, null);
        var projects = await projectActions.FetchAllProjects(request, new FetchAllProjectsRequest());
        
        return projects.Projects
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id, x => x.Name);
    }
}