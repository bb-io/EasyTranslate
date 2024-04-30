using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class FolderRequest
{
    [Display("Folder ID"), DataSource(typeof(FolderDataHandler))]
    public string FolderId { get; set; }
}