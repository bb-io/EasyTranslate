using Apps.EasyTranslate.Api;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Requests;
using Apps.EasyTranslate.Models.Responses.Tasks;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;
using System.Net;

namespace Apps.EasyTranslate.Actions;

[ActionList("Content")]
public class ContentActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : AppInvocable(invocationContext)
{
    [Action("Download content", Description = "Download source or target content based on provided URL")]
    public async Task<DownloadTargetContentResponse> DownloadTargetContent([ActionParameter] DownloadTargetContentRequest downloadRequest)
    {
        if (!Uri.IsWellFormedUriString(downloadRequest.ContentUrl, UriKind.Absolute))
        {
            throw new PluginMisconfigurationException("Content url must be a valid absolute URL. Please provide a full URL (e.g., https://api.easytranslate.com/content/{id}) and try again");
        }
        var token = await Client.GetToken(Creds);

        var request = new EasyTranslateRequest(new()
        {
            Url = downloadRequest.ContentUrl,
            Method = Method.Get
        }, token);

        var response = await Client.ExecuteRequest(request);

        var bytes = response.RawBytes ?? throw new PluginApplicationException("No content found");
        var memoryStream = new MemoryStream(bytes);

        string fileName = downloadRequest.FileName ?? $"target-content-{DateTime.Now}.json";
        var fileReference = await fileManagementClient.UploadAsync(memoryStream, ContentType.Json, fileName);
        return new DownloadTargetContentResponse { File = fileReference };
    }
}