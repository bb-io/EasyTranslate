using Apps.EasyTranslate.Constants;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Accounts;
using Apps.EasyTranslate.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.EasyTranslate.DataSourceHandlers;

public class TargetLanguageDataHandler(InvocationContext invocationContext, [ActionParameter] CreateProjectRequest request)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.SourceLanguage))
        {
            throw new InvalidOperationException("You should first select a source language");
        }

        var responses = await Client.ExecuteWithJson<GetAccountDto>(
            ApiEndpoints.TeamBase + $"/[teamname]", Method.Get, null,
            Creds);

        var languagePairs = responses.Data.Attributes.LanguagePairs;
        
        if(languagePairs.Translation.TryGetValue(request.SourceLanguage, out var sourceLanguagePair))
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