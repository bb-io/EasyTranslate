using Apps.EasyTranslate.Constants;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Accounts;
using Apps.EasyTranslate.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.EasyTranslate.DataSourceHandlers;

public class WorkflowDataHandler(InvocationContext invocationContext, [ActionParameter] CreateProjectRequest request)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.TeamName))
        {
            throw new InvalidOperationException("You should first select a team");
        }

        var responses = await Client.ExecuteWithJson<GetAccountDto>(
            ApiEndpoints.TeamBase + $"/{request.TeamName}", Method.Get, null,
            Creds);

        var workflows = responses.Data.Attributes.Workflows;
        
        return workflows
            .Where(x => context.SearchString == null ||
                        x.Attributes.DisplayName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id, x => x.Attributes.DisplayName);
    }
}