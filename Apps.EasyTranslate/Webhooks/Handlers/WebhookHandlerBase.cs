using Apps.EasyTranslate.Api;
using Apps.EasyTranslate.Constants;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Webhooks;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;

namespace Apps.EasyTranslate.Webhooks.Handlers;

public abstract class WebhookHandlerBase : AppInvocable, IWebhookEventHandler
{
    protected WebhookHandlerBase(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    protected abstract List<string> SubscriptionEvents { get; }

    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
    {
        var teamName = Creds.Get(CredsNames.Teamname).Value;
        var endpoint = $"/laas/api/v1/teams/{teamName}/webhook-endpoints";

        var body = new
        {
            data = new
            {
                type = "webhook_endpoint",
                attributes = new
                {
                    url = values["payloadUrl"],
                    status = "enabled",
                    events = SubscriptionEvents
                }
            }
        };

        await Client.ExecuteWithJson(endpoint, RestSharp.Method.Post, body, authenticationCredentialsProvider.ToArray());
    }

    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
    {
        var webhooksDto = await GetWebhooksAsync(authenticationCredentialsProvider);
        var webhook = GetWebhookBasedOnPayloadUrl(values["payloadUrl"], webhooksDto);
        if (webhook == null)
        {
            return;
        }

        var teamName = Creds.Get(CredsNames.Teamname).Value;
        var endpoint = $"/laas/api/v1/teams/{teamName}/webhook-endpoints/{webhook.Id}";
        await Client.ExecuteWithJson(endpoint, RestSharp.Method.Delete, null, authenticationCredentialsProvider.ToArray());
    }

    private async Task<GetWebhooksDto> GetWebhooksAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider)
    {
        var teamName = Creds.Get(CredsNames.Teamname).Value;
        var endpoint = $"/laas/api/v1/teams/{teamName}/webhook-endpoints";
        return await Client.ExecuteWithJson<GetWebhooksDto>(endpoint, RestSharp.Method.Get, null, authenticationCredentialsProvider.ToArray());
    }

    private Data<WebhooksAttributes>? GetWebhookBasedOnPayloadUrl(string payloadUrl, GetWebhooksDto dto)
    {
        return dto.Data.FirstOrDefault(x => x.Attributes.Url == payloadUrl);
    }
}
