using Apps.EasyTranslate.Models.Dto.Generic;

namespace Apps.EasyTranslate.Models.Dto.Keys;

public class GetTranslationKeysDto
{
    public Data<TranslationKeyAttributes>[] Data { get; set; }
}