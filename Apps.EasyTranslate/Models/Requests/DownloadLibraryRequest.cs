using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class DownloadLibraryRequest : LibraryRequest
{
    [Display("Languages"), DataSource(typeof(LibraryLanguagesDataHandler))]
    public IEnumerable<string> Languages { get; set; }

    [Display("Exclude empty translations")]
    public bool ExcludeEmptyTranslations { get; set; }

    [Display("Unpack strings")]
    public bool UnpackStrings { get; set; }
}
