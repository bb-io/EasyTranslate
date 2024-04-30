using Apps.EasyTranslate.Actions;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Requests;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.DataSourceHandlers;

public class TranslationStringDataSource(InvocationContext invocationContext, [ActionParameter] TranslationStringRequest request)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.LibraryId))
        {
            throw new InvalidOperationException("You should input a library ID first");
        }

        var translationKeyActions = new TranslationStringActions(InvocationContext);
        var translationKeys = await translationKeyActions.GetTranslationStrings(request);

        return translationKeys.TranslationStrings
            .Where(x => context.SearchString == null ||
                        x.Key.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Take(20)
            .ToDictionary(x => x.Id, x => x.Key);
    }
}
