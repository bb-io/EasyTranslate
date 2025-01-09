using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Webhooks.Models.Request
{
    public class ProjectFilter
    {
        [Display("Project ID"), DataSource(typeof(ProjectDataHandler))]
        public string? ProjectId { get; set; }
    }
}
