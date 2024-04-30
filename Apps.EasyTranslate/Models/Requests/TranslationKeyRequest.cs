using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class TranslationKeyRequest : LibraryRequest
{
    [Display("Key ID"), DataSource(typeof(TranslationKeyDataSource))]
    public string KeyId { get; set; }
}