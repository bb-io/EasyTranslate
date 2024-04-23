using RestSharp;
using Apps.EasyTranslate.Constants;
using Apps.EasyTranslate.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Apps.EasyTranslate.Api;

public class EasyTranslateClient : RestClient
{
    public async Task<T> ExecuteWithJson<T>(string endpoint, Method method, object? bodyObj,
        AuthenticationCredentialsProvider[] creds)
    {
        var response = await ExecuteWithJson(endpoint, method, bodyObj, creds);
        return JsonConvert.DeserializeObject<T>(response.Content);
    }
    
    public async Task<RestResponse> ExecuteWithJson(string endpoint, Method method, object? bodyObj,
        AuthenticationCredentialsProvider[] creds)
    {
        var token = await GetToken(creds);
        var baseUrl = BuildUrl(creds);

        var request = new EasyTranslateRequest(new()
        {
            Url = baseUrl + endpoint,
            Method = method
        }, token);

        if (bodyObj is not null)
            request.WithJsonBody(bodyObj, new()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                NullValueHandling = NullValueHandling.Ignore
            });

        return await ExecuteRequest(request);
    }
    
    public async Task<string> GetToken(AuthenticationCredentialsProvider[] creds)
    {
        var url = BuildUrl(creds);
        
        var clientId = creds.Get(CredsNames.ClientId);
        var clientSecret = creds.Get(CredsNames.ClientSecret);
        var username = creds.Get(CredsNames.Username);
        var password = creds.Get(CredsNames.Password);
        var grantType = new KeyValuePair<string,string>(CredsNames.GrantType, "password");
        var scope = new KeyValuePair<string,string>(CredsNames.Scope, "dashboard");

        var request = new RestRequest(url + ApiEndpoints.Token, Method.Post);

        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

        request.AddParameter(username.KeyName, username.Value, ParameterType.GetOrPost);
        request.AddParameter(password.KeyName, password.Value, ParameterType.GetOrPost);
        request.AddParameter(clientId.KeyName, clientId.Value, ParameterType.GetOrPost);
        request.AddParameter(clientSecret.KeyName, clientSecret.Value, ParameterType.GetOrPost);
        request.AddParameter(grantType.Key, grantType.Value, ParameterType.GetOrPost);
        request.AddParameter(scope.Key, scope.Value, ParameterType.GetOrPost);

        var response = await this.ExecuteAsync<TokenResponse>(request);

        if (response.Data is null)
        {
            throw new Exception("Failed to get token");
        }

        if (string.IsNullOrEmpty(response.Data.AccessToken))
        {
            response.Data = JsonConvert.DeserializeObject<TokenResponse>(response.Content);
        }
        
        return response.Data.AccessToken;
    }
    
    public async Task<RestResponse> ExecuteRequest(EasyTranslateRequest request)
    {
        var response = await ExecuteAsync(request);

        if (!response.IsSuccessStatusCode)
            throw GetError(response);

        return response;
    }
    
    private Exception GetError(RestResponse response)
    {
        return new Exception($"Status code: {response.StatusCode}, Message: {response.Content}");
    }

    private string BuildUrl(AuthenticationCredentialsProvider[] creds)
    {
        var host = creds.Get(CredsNames.Host);
        return $"https://{host.Value.TrimEnd('/')}";
    }
}