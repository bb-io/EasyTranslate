using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto;


public class TranslationStringDto
{
    [JsonProperty("translation_id")]
    public string TranslationId { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }
}
