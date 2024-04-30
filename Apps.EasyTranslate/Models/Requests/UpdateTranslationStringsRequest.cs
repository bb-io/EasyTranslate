using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Requests;

public class UpdateTranslationStringsRequest : TranslationStringRequest
{
    public IEnumerable<string> Text { get; set; }

    [Display("Translation string IDs")]
    public IEnumerable<string>? Ids { get; set; }
}
