using Apps.EasyTranslate.Actions;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.EasyTranslate.DataSourceHandlers;

public class TaskDataHandler(InvocationContext invocationContext, [ActionParameter] TaskRequest request)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.TeamName))
        {
            throw new InvalidOperationException("You should input a team name first");
        }
        
        if(string.IsNullOrEmpty(request.ProjectId))
        {
            throw new InvalidOperationException("You should first select a project");
        }
        
        var taskActions = new TaskActions(InvocationContext, null);
        var tasks = await taskActions.GetAllTasks(request);
        
        return tasks.Tasks
            .Where(x => context.SearchString == null ||
                        x.Type.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.TaskId, x => x.Type);
    }
}