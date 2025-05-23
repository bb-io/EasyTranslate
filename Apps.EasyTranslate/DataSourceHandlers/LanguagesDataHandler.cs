using Apps.EasyTranslate.Actions;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.EasyTranslate.DataSourceHandlers;

public class LanguagesDataHandler(InvocationContext invocationContext, [ActionParameter] LibraryRequest request)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.LibraryId))
        {
            throw new PluginMisconfigurationException("You should select a library first");
        }
        
        var libraryActions = new LibraryActions(InvocationContext, null);
        var libraries = await libraryActions.GetLibrary(request);
        
        return libraries
            .Languages
            .Where(x => context.SearchString == null ||
                        x.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x, x => x);
    }
}