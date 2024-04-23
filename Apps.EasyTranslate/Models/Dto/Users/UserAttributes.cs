using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Users;

public class UserAttributes
{
    [JsonProperty("company_name")]
    public string CompanyName { get; set; }
    
    [JsonProperty("company_email")]
    public string CompanyEmail { get; set; }
}