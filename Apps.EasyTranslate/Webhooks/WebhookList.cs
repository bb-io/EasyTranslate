﻿using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto;
using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Projects;
using Apps.EasyTranslate.Models.Dto.Tasks;
using Apps.EasyTranslate.Models.Responses.Projects;
using Apps.EasyTranslate.Webhooks.Handlers;
using Apps.EasyTranslate.Webhooks.Models.Payload.TaskUpdated;
using Apps.EasyTranslate.Webhooks.Models.Responses;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using Apps.EasyTranslate.Webhooks.Models.Payload.StringKeyUpdated;
using Apps.EasyTranslate.Webhooks.Models.Request;
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
    public async Task<WebhookResponse<TaskUpdatedResponse>> OnTaskUpdated(WebhookRequest webhookRequest,
        [WebhookParameter] TaskFilter filter)
    {
        var payload = HandleWebhook<TaskUpdatedPayload>(webhookRequest);

        if ((!string.IsNullOrEmpty(filter.ProjectId) && filter.ProjectId != payload.Data.Attributes.Project.Id)
         || (!string.IsNullOrEmpty(filter.TaskId) && filter.TaskId != payload.Data.Id)
         || (!string.IsNullOrEmpty(filter.SupplierId) && filter.SupplierId != payload.Data.Attributes.SupplierId))
        {
            return new WebhookResponse<TaskUpdatedResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            };
        }
        var response = new TaskUpdatedResponse(payload.Data);
        return new WebhookResponse<TaskUpdatedResponse>
        {
            Result = response
        };
    }

    [Webhook("On project price accepted", typeof(ProjectPriceAcceptedHandler), Description = "Triggered when a project price accepted")]
    public async Task<WebhookResponse<ProjectResponse>> OnProjectUpdated(WebhookRequest webhookRequest,
        [WebhookParameter] ProjectFilter filter)
    {
        var payload = HandleWebhook<ProjectDto>(webhookRequest);
        if (!string.IsNullOrEmpty(filter.ProjectId) && filter.ProjectId != payload.Data.Id)
        {
            return new WebhookResponse<ProjectResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            };
        }
        var response = new ProjectResponse(payload.Data);
        return new WebhookResponse<ProjectResponse>
        {
            Result = response
        };
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
    
    [Webhook("On project approval needed", typeof(ProjectApprovalNeededHandler), Description = "Triggered when a project approval needed")]
    public async Task<WebhookResponse<ProjectResponse>> OnProjectApprovalNeeded(WebhookRequest webhookRequest,
        [WebhookParameter] ProjectFilter filter)
    {
        var payload = HandleWebhook<ProjectDto>(webhookRequest);
        if (!string.IsNullOrEmpty(filter.ProjectId)
        && filter.ProjectId != payload.Data.Id)
        {
            return new WebhookResponse<ProjectResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            };
        }
        var response = new ProjectResponse(payload.Data);
        return new WebhookResponse<ProjectResponse>
        {
            Result = response
        };
    }
    
    [Webhook("On project price declined", typeof(ProjectPriceDeclinedHandler), Description = "Triggered when a project price declined")]
    public async Task<WebhookResponse<ProjectV1Response>> OnProjectPriceDeclined(WebhookRequest webhookRequest,
    [WebhookParameter] ProjectFilter filter)
    {
        var payload = HandleWebhook<DataDto<Data<V1ProjectAttributes>, TaskAttributes>>(webhookRequest);
        if (!string.IsNullOrEmpty(filter.ProjectId)
        && filter.ProjectId != payload.Data.Id)
        {
            return new WebhookResponse<ProjectV1Response>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            };
        }

        return new WebhookResponse<ProjectV1Response>
        {
            Result = new ProjectV1Response(payload)
        };
    }

    [Webhook("On project cancelled", typeof(ProjectCancelledHandler), Description = "Triggered when a project cancelled by customer")]
    public async Task<WebhookResponse<ProjectResponse>> OnProjectCancelled(WebhookRequest webhookRequest,
        [WebhookParameter] ProjectFilter filter)
    {
        var payload = HandleWebhook<ProjectDto>(webhookRequest);
        if (!string.IsNullOrEmpty(filter.ProjectId)
        && filter.ProjectId != payload.Data.Id)
        {
            return new WebhookResponse<ProjectResponse>
            {
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
                ReceivedWebhookRequestType = WebhookRequestType.Preflight
            };
        }
        var response = new ProjectResponse(payload.Data);
        return new WebhookResponse<ProjectResponse>
        {
            Result = response
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
            InvocationContext.Logger?.LogError($"Deserialization failed. Body: {webhookRequest.Body}", [webhookRequest.Body.ToString()]);
            throw new InvalidCastException($"Deserialization failed. Body: {webhookRequest.Body}");
        }

        return data;
    }

    #endregion
}
