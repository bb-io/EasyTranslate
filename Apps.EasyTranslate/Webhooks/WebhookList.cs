using Apps.EasyTranslate.Api;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Projects;
using Apps.EasyTranslate.Models.Responses.Projects;
using Apps.EasyTranslate.Webhooks.Handlers;
using Apps.EasyTranslate.Webhooks.Models.Payload.TaskUpdated;
using Apps.EasyTranslate.Webhooks.Models.Responses;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Apps.EasyTranslate.Webhooks;

[WebhookList]
public class WebhookList : AppInvocable
{
    public WebhookList(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    #region Webhooks

    [Webhook("On task updated", typeof(TaskUpdatedHandler), Description = "Triggered when a task updated")]
    public async Task<WebhookResponse<TaskUpdatedResponse>> OnTaskUpdated(WebhookRequest webhookRequest)
    {
        var response = HandleWebhook<TaskUpdatedPayload>(webhookRequest);
        return new WebhookResponse<TaskUpdatedResponse>
        {
            Result = new TaskUpdatedResponse(response.Data)
        };
    }

    [Webhook("On project updated", typeof(ProjectUpdatedHandler), Description = "Triggered when a project updated")]
    public async Task<WebhookResponse<ProjectResponse>> OnProjectUpdated(WebhookRequest webhookRequest)
    {
        var response = HandleWebhook<ProjectDto>(webhookRequest);
        return new WebhookResponse<ProjectResponse>
        {
            Result = new ProjectResponse(response.Data)
        };
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
