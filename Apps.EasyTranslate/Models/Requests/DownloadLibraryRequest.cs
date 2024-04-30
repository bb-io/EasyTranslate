using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class DownloadLibraryRequest : LibraryRequest
{
    [Display("Languages"), DataSource(typeof(LibraryLanguagesDataHandler))]
    public IEnumerable<string> Languages { get; set; }

    [Display("Exclude empty translations", Description = "By default: false")]
    public bool? ExcludeEmptyTranslations { get; set; }

    [Display("Unpack strings", Description = "By default: false")]
    public bool? UnpackStrings { get; set; }
}
