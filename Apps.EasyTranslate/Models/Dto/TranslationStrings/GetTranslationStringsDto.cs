using Apps.EasyTranslate.Models.Dto.Generic;

namespace Apps.EasyTranslate.Models.Dto.TranslationStrings;

public class GetTranslationStringsDto
{
    public Data<TranslationStringAttributes>[] Data { get; set; }
}
