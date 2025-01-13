using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Webhooks.Models.Request
{
    public class TaskFilter
    {
        [Display("Project ID"), DataSource(typeof(ProjectDataHandler))]
        public string? ProjectId { get; set; }

        [Display("Task ID"), DataSource(typeof(TaskDataHandler))]
        public string? TaskId { get; set; }

        [Display("Supplier ID")]
        public string? SupplierId { get; set; }
    }
}
