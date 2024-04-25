using RestSharp;
using Apps.EasyTranslate.Constants;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Users;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.EasyTranslate.DataSourceHandlers;

public class TeamDataHandler(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var respones = await Client.ExecuteWithJson<GetAuthenticatedUsersResponse>(ApiEndpoints.User, Method.Get, null,
            Creds);

        var attributes = respones.Included.Select(x => x.Attributes).ToList();
        
        return attributes
            .Where(x => context.SearchString == null ||
                        x.TeamIdentifier.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.TeamIdentifier, x => x.TeamIdentifier);
    }
}