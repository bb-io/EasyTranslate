using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Workflows;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Users;

public class AccountAttributes
{
    [JsonProperty("company_name")]
    public string CompanyName { get; set; }

    [JsonProperty("company_email")]
    public string CompanyEmail { get; set; }

    [JsonProperty("team_identifier")]
    public string TeamIdentifier { get; set; }

    [JsonProperty("workflows")]
    public Data<WorkflowAttributes>[] Workflows { get; set; }
}