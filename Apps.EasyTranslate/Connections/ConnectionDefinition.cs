using Apps.EasyTranslate.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.EasyTranslate.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    private static IEnumerable<ConnectionProperty> ConnectionProperties => new[]
    {
        new ConnectionProperty(CredsNames.Host)
        {
            DisplayName = "Host", Description = "Host of the Easytranslate API, e.g.: api.platform.sandbox.easytranslate.com"
        },
        new ConnectionProperty(CredsNames.ClientId)
        {
            DisplayName = "Client ID", Description = "Client ID provided by Easytranslate"
        },
        new(CredsNames.ClientSecret)
        {
            DisplayName = "Client Secret", Description = "Client secret provided by Easytranslate", Sensitive = true
        },
        new(CredsNames.Username)
        {
            DisplayName = "Username", Description = "Username/email of the User who will perform the actions"
        },
        new(CredsNames.Password)
        {
            DisplayName = "Password", Description = "Password of the User who will perform the actions", Sensitive = true
        }
    };
    
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        new()
        {
            Name = "Developer API key",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionUsage = ConnectionUsage.Actions,
            ConnectionProperties = ConnectionProperties
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        var clientIdKeyValue = values.First(v => v.Key == CredsNames.ClientId);
        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.None,
            clientIdKeyValue.Key,
            clientIdKeyValue.Value
        );
        
        var secretKeyValue = values.First(v => v.Key == CredsNames.ClientSecret);
        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.None,
            secretKeyValue.Key,
            secretKeyValue.Value
        );
        
        var usernameKeyValue = values.First(v => v.Key == CredsNames.Username);
        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.None,
            usernameKeyValue.Key,
            usernameKeyValue.Value
        );
        
        var passwordKeyValue = values.First(v => v.Key == CredsNames.Password);
        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.None,
            passwordKeyValue.Key,
            passwordKeyValue.Value
        );
    }
}