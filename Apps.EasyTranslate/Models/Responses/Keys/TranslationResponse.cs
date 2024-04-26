using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Responses.Keys;

public class TranslationResponse
{
    [Display("Translation ID")]
    public string Id { get; set; }

    public string Text { get; set; }

    public string Status { get; set; }
    
    [Display("Language code")]
    public string LanguageCode { get; set; }
}