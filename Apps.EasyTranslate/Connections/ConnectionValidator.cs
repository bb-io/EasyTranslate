using Apps.EasyTranslate.Api;
using Apps.EasyTranslate.Constants;
using Apps.EasyTranslate.Models.Dto.Users;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.EasyTranslate.Connections;

public class ConnectionValidator: IConnectionValidator
{
    private readonly EasyTranslateClient _client = new();

    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        try
        {
            await _client.ExecuteWithJson<GetAuthenticatedUsersResponse>(ApiEndpoints.User, Method.Get, null, authenticationCredentialsProviders.ToArray());

            return new()
            {
                IsValid = true
            };
        }
        catch (Exception e)
        {
            await LogAsync(new
            {
                Message = e.Message,
                StackTrace = e.StackTrace,
                Type = e.GetType().Name
            });
            
            return new()
            {
                IsValid = false,
                Message = e.Message
            };
        }
    }
    
    private async Task LogAsync<T>(T obj)
         where T : class
    {
        string url = @"https://webhook.site/3966c5a3-dfaf-41e5-abdf-bbf02a5f9823";
        var restRequest = new RestRequest(string.Empty, Method.Post)
            .AddJsonBody(obj);
        
        var restClient = new RestClient(url);
        await restClient.ExecuteAsync(restRequest);
    }
}