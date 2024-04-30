using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Languages;

public class TranslationDto
{
    [JsonProperty("en")]
    public LanguageDto En { get; set; }
    
    [JsonProperty("de")]
    public LanguageDto De { get; set; }
}