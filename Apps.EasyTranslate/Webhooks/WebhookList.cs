using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Projects;
using Apps.EasyTranslate.Models.Responses.Projects;
using Apps.EasyTranslate.Webhooks.Handlers;
using Apps.EasyTranslate.Webhooks.Models.Payload.TaskUpdated;
using Apps.EasyTranslate.Webhooks.Models.Responses;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using Apps.EasyTranslate.Webhooks.Models.Payload.StringKeyUpdated;

namespace Apps.EasyTranslate.Webhooks;

[WebhookList]
public class WebhookList : AppInvocable
{
    public WebhookList(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    #region Webhooks

    [Webhook("On task updated", typeof(TaskUpdatedHandler), Description = "Triggered when a task updated")]
    public Task<WebhookResponse<TaskUpdatedResponse>> OnTaskUpdated(WebhookRequest webhookRequest)
    {
        var response = HandleWebhook<TaskUpdatedPayload>(webhookRequest);
        return Task.FromResult(new WebhookResponse<TaskUpdatedResponse>
        {
            Result = new TaskUpdatedResponse(response.Data)
        });
    }

    [Webhook("On project price accepted", typeof(ProjectUpdatedHandler), Description = "Triggered when a project price accepted")]
    public Task<WebhookResponse<ProjectResponse>> OnProjectUpdated(WebhookRequest webhookRequest)
    {
        var response = HandleWebhook<ProjectDto>(webhookRequest);
        return Task.FromResult(new WebhookResponse<ProjectResponse>
        {
            Result = new ProjectResponse(response.Data)
        });
    }
    
    [Webhook("On string key updated", typeof(StringKeyUpdatedHandler), Description = "Triggered when a string key updated")]
    public Task<WebhookResponse<StringKeyUpdatedResponses>> OnStringKeyUpdated(WebhookRequest webhookRequest)
    {
        var response = HandleWebhook<StringKeyUpdatedPayload>(webhookRequest);
        return Task.FromResult(new WebhookResponse<StringKeyUpdatedResponses>
        {
            Result = new StringKeyUpdatedResponses(response)
        });
    }

    #endregion

    #region Utils

    private T HandleWebhook<T>(WebhookRequest webhookRequest)
        where T : class
    {
        var data = JsonConvert.DeserializeObject<T>(webhookRequest.Body.ToString());
        if (data is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }

        return data;
    }

    #endregion
}
