using Apps.EasyTranslate.Api;
using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Webhooks;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.EasyTranslate.Webhooks.Handlers;

public abstract class WebhookHandlerBase : IWebhookEventHandler
{
    private readonly EasyTranslateClient _easyTranslateClient;

    protected abstract string SubscriptionEvent { get; }

    protected string TeamName { get; } = "testingblackbirdcom";

    protected WebhookHandlerBase()
    {
        _easyTranslateClient = new EasyTranslateClient();
    }

    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
    {
        var endpoint = $"/laas/api/v1/teams/{TeamName}/webhook-endpoints";

        var body = new
        {
            data = new
            {
                type = "webhook_endpoint",
                attributes = new
                {
                    url = values["payloadUrl"],
                    status = "enabled",
                    events = new[] { SubscriptionEvent }
                }
            }
        };

        await _easyTranslateClient.ExecuteWithJson(endpoint, RestSharp.Method.Post, body, authenticationCredentialsProvider.ToArray());
    }

    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
    {
        var webhooksDto = await GetWebhooksAsync(authenticationCredentialsProvider);
        var webhook = GetWebhookBasedOnPayloadUrl(values["payloadUrl"], webhooksDto);
        if (webhook == null)
        {
            return;
        }

        var endpoint = $"/laas/api/v1/teams/{TeamName}/webhook-endpoints/{webhook.Id}";
        await _easyTranslateClient.ExecuteWithJson(endpoint, RestSharp.Method.Delete, null, authenticationCredentialsProvider.ToArray());
    }

    private async Task<GetWebhooksDto> GetWebhooksAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider)
    {
        var endpoint = $"/laas/api/v1/teams/{TeamName}/webhook-endpoints";
        return await _easyTranslateClient.ExecuteWithJson<GetWebhooksDto>(endpoint, RestSharp.Method.Get, null, authenticationCredentialsProvider.ToArray());
    }

    private Data<WebhooksAttributes>? GetWebhookBasedOnPayloadUrl(string payloadUrl, GetWebhooksDto dto)
    {
        return dto.Data.FirstOrDefault(x => x.Attributes.Url == payloadUrl);
    }
}
