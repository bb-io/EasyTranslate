using Apps.EasyTranslate.Models.Dto.Keys;

namespace Apps.EasyTranslate.Models.Responses.Keys;

public class TranslationKeysResponse
{
    public List<TranslationKeyResponse> Keys { get; set; }
    
    public TranslationKeysResponse(GetTranslationKeysDto dto)
    {
        Keys = dto.Data.Select(x => new TranslationKeyResponse(x)).ToList();
    }
}