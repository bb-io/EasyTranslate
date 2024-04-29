using Apps.EasyTranslate.Models.Requests;

namespace Apps.EasyTranslate.Models.Dto.TranslationStrings;

public class UpdateTranslationStringRequest : TranslationStringRequest
{
    public string Text { get; set; }
}
