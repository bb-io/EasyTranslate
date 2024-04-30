using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Languages;

public class LanguagePairsDto
{
    [JsonProperty("translation")]
    public Dictionary<string, LanguageDto> Translation { get; set; }
}