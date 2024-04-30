using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class LibraryAutomationRequest : LibraryRequest
{
    [Display("Target languages"), DataSource(typeof(LanguagesDataHandler))]
    public IEnumerable<string> TargetLanguages { get; set; }

    [Display("Key names")] public IEnumerable<string>? KeyNames { get; set; }
}