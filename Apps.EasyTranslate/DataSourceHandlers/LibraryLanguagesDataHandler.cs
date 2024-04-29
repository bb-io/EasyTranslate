using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Requests;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common;
using Apps.EasyTranslate.Actions;

namespace Apps.EasyTranslate.DataSourceHandlers;

public class LibraryLanguagesDataHandler(InvocationContext invocationContext, [ActionParameter] LibraryRequest request)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.TeamName))
        {
            throw new InvalidOperationException("You should first select a team");
        }

        if (string.IsNullOrEmpty(request.LibraryId))
        {
            throw new InvalidOperationException("You should first select a library");
        }

        var libraryActions = new LibraryActions(InvocationContext, null);
        var library = await libraryActions.GetLibrary(request);

        return library.Languages
            .Where(x => context.SearchString == null ||
                                   x.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x, x => x);
    }
}