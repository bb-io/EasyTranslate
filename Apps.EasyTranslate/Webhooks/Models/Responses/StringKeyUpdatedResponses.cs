using Apps.EasyTranslate.Webhooks.Models.Payload.StringKeyUpdated;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Webhooks.Models.Responses;

public class StringKeyUpdatedResponses
{
    [Display("String keys")]
    public List<StringKeyUpdatedResponse> StringKeys { get; set; }

    public StringKeyUpdatedResponses(StringKeyUpdatedPayload payload)
    {
        StringKeys = payload.Data.Select(x => new StringKeyUpdatedResponse(x)).ToList();
    }
}