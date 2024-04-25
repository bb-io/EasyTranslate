using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class TeamRequest
{
    [Display("Team name"), DataSource(typeof(TeamDataHandler))]
    public string TeamName { get; set; }
}
