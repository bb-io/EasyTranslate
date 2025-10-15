using RestSharp;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Requests;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common;
using Apps.EasyTranslate.Models.Responses.TranslationStrings;
using Apps.EasyTranslate.Models.Dto.TranslationStrings;
using Apps.EasyTranslate.Models.Dto;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.EasyTranslate.Actions;

[ActionList("Translation strings")]
public class TranslationStringActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Get translation strings", Description = "Get all translation strings for specific library")]
    public async Task<TranslationStringsResponse> GetTranslationStrings([ActionParameter] LibraryRequest request)
    {
        var endpoint = $"/strings-library/api/v1/teams/[teamname]/libraries/{request.LibraryId}/translations";
        var dto = await Client.ExecuteWithJson<GetTranslationStringsDto>(endpoint, Method.Get, null, Creds);
        return new TranslationStringsResponse(dto);
    }

    [Action("Get translation string", Description = "Get translation string for specific library")]
    public async Task<TranslationStringResponse> GetTranslationString([ActionParameter] TranslationStringRequest request)
    {
        var endpoint = $"/strings-library/api/v1/teams/[teamname]/libraries/{request.LibraryId}/translations/{request.TranslationStringId}";
        var dto = await Client.ExecuteWithJson<GetTranslationStringDto>(endpoint, Method.Get, null, Creds);
        return new TranslationStringResponse(dto.Data);
    }

    [Action("Update translation string", Description = "Update translation string for specific library")]
    public async Task<TranslationStringResponse> UpdateTranslationString([ActionParameter] UpdateTranslationStringRequest request)
    {
        var endpoint = $"/strings-library/api/v1/teams/[teamname]/libraries/{request.LibraryId}/translations/{request.TranslationStringId}";

        var body = new
        {
            data = new
            {
                type = "translation",
                attributes = new
                {
                    text = request.Text
                }
            }
        };

        var dto = await Client.ExecuteWithJson<GetTranslationStringDto>(endpoint, Method.Put, body, Creds);
        return new TranslationStringResponse(dto.Data);
    }

    [Action("Update translation strings", Description = "Update translation strings for specific library")]
    public async Task<TranslationStringsResponse> UpdateTranslationStrings([ActionParameter] UpdateTranslationStringsRequest request)
    {
        var endpoint = $"/strings-library/api/v1/teams/[teamname]/libraries/{request.LibraryId}/translations";

        var ids = request.Ids?.Select(id => Guid.Parse(id)).ToList();
        var texts = request.Text.ToList();

        var strings = BuildTranslationStrings(ids, texts);

        var body = new
        {
            data = new
            {
                type = "translations",
                attributes = new
                {
                    strings = strings
                }
            }
        };

        var dto = await Client.ExecuteWithJson<GetTranslationStringsDto>(endpoint, Method.Put, body, Creds);
        return new TranslationStringsResponse(dto);
    }

    private List<TranslationStringDto> BuildTranslationStrings(List<Guid>? ids, List<string> texts)
    {
        var strings = new List<TranslationStringDto>();

        if (ids != null && ids.Any())
        {
            if (ids.Count != texts.Count)
            {
                throw new PluginMisconfigurationException("Ids and Texts count must be equal or don't specify ids");
            }

            for (int i = 0; i < ids.Count; i++)
            {
                strings.Add(new TranslationStringDto
                {
                    TranslationId = ids[i].ToString(),
                    Text = texts[i]
                });
            }
        }
        else
        {
            for (int i = 0; i < texts.Count; i++)
            {
                strings.Add(new TranslationStringDto
                {
                    TranslationId = Guid.NewGuid().ToString(),
                    Text = texts[i]
                });
            }
        }

        return strings;
    }
}
