using Apps.EasyTranslate.Models.Dto.Generic;

namespace Apps.EasyTranslate.Models.Dto.Keys;

public class GetTranslationKeyDto
{
    public Data<TranslationKeyAttributes> Data { get; set; }
}
