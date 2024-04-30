using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class UpdateFolderRequest
{
    public string Name { get; set; }

    [Display("Included project IDs"), DataSource(typeof(ProjectDataHandler))]
    public IEnumerable<string>? IncludedProjectIds { get; set; }
    
    [Display("Excluded project IDs"), DataSource(typeof(ProjectDataHandler))]
    public IEnumerable<string>? ExcludedProjectIds { get; set; }
}