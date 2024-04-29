using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class TranslationStringRequest : LibraryRequest
{
    [Display("Translation string ID"), DataSource(typeof(TranslationStringDataSource))]
    public string TranslationStringId { get; set; }
}
