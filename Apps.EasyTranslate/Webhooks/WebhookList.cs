using Apps.EasyTranslate.Api;
using Apps.EasyTranslate.Invocables;
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
    private readonly IFileManagementClient fileManagementClient;

    public WebhookList(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(invocationContext)
    {
        this.fileManagementClient = fileManagementClient;
    }

    #region Webhooks

    [Webhook("On string key updated", typeof(StringKeyUpdatedHandler), Description = "Triggered when a string key changed")]
    public async Task<WebhookResponse<StringKeyResponse>> OnTermChanged(WebhookRequest webhookRequest)
    {
        var response = HandleWebhook<StringKeyUpdatedPayload>(webhookRequest);

        var fileReference = await DownloadTargetContentAsync(response);
        return new WebhookResponse<StringKeyResponse>
        {
            Result = new StringKeyResponse(response.Data)
            {
                TargetContent = fileReference
            }
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

    private async Task<FileReference> DownloadTargetContentAsync(StringKeyUpdatedPayload payload)
    {
        var token = await Client.GetToken(Creds);

        var request = new EasyTranslateRequest(new()
        {
            Url = payload.Data.Attributes.TargetContent,
            Method = Method.Get
        }, token);

        var response = await Client.ExecuteRequest(request);

        var bytes = response.RawBytes ?? throw new WebException("No content found");
        var memoryStream = new MemoryStream(bytes);

        var fileReference = await fileManagementClient.UploadAsync(memoryStream, ContentType.Json, payload.Data.Attributes.FileName);
        return fileReference;
    }

    #endregion
}
