using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto;
using Apps.EasyTranslate.Models.Dto.Keys;
using Apps.EasyTranslate.Models.Requests;
using Apps.EasyTranslate.Models.Responses.Keys;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.EasyTranslate.Actions;

[ActionList]
public class TranslationKeyActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get translation keys", Description = "Get translation keys for specified library")]
    public async Task<TranslationKeysResponse> GetTranslationKeys([ActionParameter] LibraryRequest request)
    {
        string endpoint = $"/strings-library/api/v1/teams/[teamname]/libraries/{request.LibraryId}/keys";
        var dto = await Client.ExecuteWithJson<GetTranslationKeysDto>(endpoint, Method.Get, null, Creds);
        return new TranslationKeysResponse(dto);
    }
    
    [Action("Get translation key", Description = "Get translation key for specified library")]
    public async Task<TranslationKeyResponse> GetTranslationKey([ActionParameter] TranslationKeyRequest request)
    {
        string endpoint = $"/strings-library/api/v1/teams/[teamname]/libraries/{request.LibraryId}/keys/{request.KeyId}";
        var dto = await Client.ExecuteWithJson<GetTranslationKeyDto>(endpoint, Method.Get, null, Creds);
        return new TranslationKeyResponse(dto.Data);
    }
    
    [Action("Create translation keys", Description = "Create translation keys for specified library")]
    public async Task<TranslationKeysResponse> CreateTranslationKeys([ActionParameter] CreateTranslationKeysRequest request)
    {
        string endpoint = $"/strings-library/api/v1/teams/[teamname]/libraries/{request.LibraryId}/keys";

        var body = new
        {
            data = new
            {
                type = "translation-key",
                attributes = BuildAttributes(request)
            }
        };
        
        var dto = await Client.ExecuteWithJson<GetTranslationKeysDto>(endpoint, Method.Post, body, Creds);
        return new TranslationKeysResponse(dto);
    }
    
    [Action("Create translation key", Description = "Create translation key for specified library")]
    public async Task<TranslationKeyResponse> CreateTranslationKey([ActionParameter] CreateTranslationKeyRequest request)
    {
        var keys = await CreateTranslationKeys(new CreateTranslationKeysRequest
        {
            LibraryId = request.LibraryId,
            Names = new [] { request.Name },
            Texts = new [] { request.Text }
        });
        
        return keys.Keys.First();
    }
    
    [Action("Delete translation key", Description = "Delete translation key for specified library")]
    public async Task DeleteTranslationKey([ActionParameter] TranslationKeyRequest request)
    {
        string endpoint = $"/strings-library/api/v1/teams/[teamname]/libraries/{request.LibraryId}/keys/{request.KeyId}";
        await Client.ExecuteWithJson(endpoint, Method.Delete, null, Creds);
    }

    private List<KeyDto> BuildAttributes(CreateTranslationKeysRequest request)
    {
        var attributes = new List<KeyDto>();
        
        if (request.Names.Count() != request.Texts.Count())
        {
            throw new ArgumentException("Names and Texts must have the same number of elements");
        }
        
        for (var i = 0; i < request.Names.Count(); i++)
        {
            attributes.Add(new KeyDto
            {
                Name = request.Names.ElementAt(i),
                Text = request.Texts.ElementAt(i)
            });
        }
        
        return attributes;
    }
}