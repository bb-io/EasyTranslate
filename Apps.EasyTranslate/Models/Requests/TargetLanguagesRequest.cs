using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class TargetLanguagesRequest : TeamRequest
{
    [Display("Library ID"), DataSource(typeof(LibraryDataHandler))]
    public string LibraryId { get; set; }
    
    [Display("Target languages"), DataSource(typeof(TargetLanguageByLibraryDataHandler))]
    public IEnumerable<string> TargetLanguages { get; set; }
}