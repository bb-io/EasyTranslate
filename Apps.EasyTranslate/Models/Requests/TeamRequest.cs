using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Requests;

public class TeamRequest
{
    [Display("Team name")]
    public string TeamName { get; set; }
}
