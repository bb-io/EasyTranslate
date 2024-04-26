using Apps.EasyTranslate.Actions;
using Apps.EasyTranslate.Constants;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Accounts;
using Apps.EasyTranslate.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.EasyTranslate.DataSourceHandlers;

public class TargetLanguageByLibraryDataHandler(InvocationContext invocationContext, [ActionParameter] TargetLanguagesRequest request)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(request.TeamName))
        {
            throw new InvalidOperationException("You should first select a team");
        }
        
        if (string.IsNullOrEmpty(request.LibraryId))
        {
            throw new InvalidOperationException("You should first select a library");
        }

        var libraryActions = new LibraryActions(InvocationContext);
        var library = await libraryActions.GetLibrary(new LibraryRequest
        {
            TeamName = request.TeamName,
            LibraryId = request.LibraryId
        });
        
        var responses = await Client.ExecuteWithJson<GetAccountDto>(
            ApiEndpoints.TeamBase + $"/{request.TeamName}", Method.Get, null,
            Creds);

        var languagePairs = responses.Data.Attributes.LanguagePairs;
        if(languagePairs.Translation.TryGetValue(library.SourceLanguage, out var sourceLanguagePair))
        {
            return sourceLanguagePair.TargetLanguages
                .Where(x => context.SearchString == null ||
                            x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Take(20)
                .ToDictionary(x => x.Code, x => x.Name);
        } 
        
        throw new InvalidOperationException("Source language not found in team language pairs");
    }
}