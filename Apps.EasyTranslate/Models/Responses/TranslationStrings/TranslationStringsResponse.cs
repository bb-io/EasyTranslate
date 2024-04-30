using Apps.EasyTranslate.Models.Dto.TranslationStrings;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Responses.TranslationStrings;

public class TranslationStringsResponse
{
    [Display("Translation strings")]
    public List<TranslationStringResponse> TranslationStrings { get; set; }

    public TranslationStringsResponse(GetTranslationStringsDto dto)
    {
        TranslationStrings = dto.Data.Select(x => new TranslationStringResponse(x)).ToList();
    }
}
