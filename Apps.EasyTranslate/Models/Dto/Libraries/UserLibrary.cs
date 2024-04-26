using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Libraries;

public class UserLibrary
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("first_name")]
    public string FirstName { get; set; }
    
    [JsonProperty("last_name")]
    public string LastName { get; set; }
}