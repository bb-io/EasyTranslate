using Apps.EasyTranslate.Api;
using Apps.EasyTranslate.Constants;
using Apps.EasyTranslate.Models.Responses.Users;
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
            return new()
            {
                IsValid = false,
                Message = e.Message
            };
        }
    }
}