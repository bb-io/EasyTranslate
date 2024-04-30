using Apps.EasyTranslate.Actions;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.EasyTranslate.DataSourceHandlers;

public class LibraryDataHandler(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var libraryActions = new LibraryActions(InvocationContext, null);
        var libraries = await libraryActions.GetAllLibraries();
        
        return libraries.Libraries
            .Where(x => context.SearchString == null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id, x => x.Name);
    }
}