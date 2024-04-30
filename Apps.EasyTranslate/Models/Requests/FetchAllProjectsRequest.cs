using Apps.EasyTranslate.DataSourceHandlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.EasyTranslate.Models.Requests;

public class FetchAllProjectsRequest
{
    [Display("Statuses"), StaticDataSource(typeof(ProjectStatusStaticDataSource))]
    public IEnumerable<string>? Statuses { get; set; }
}