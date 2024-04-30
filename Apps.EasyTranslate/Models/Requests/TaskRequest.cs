using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class TaskRequest : ProjectRequest
{
    [Display("Task ID"), DataSource(typeof(TaskDataHandler))]
    public string TaskId { get; set; }
}