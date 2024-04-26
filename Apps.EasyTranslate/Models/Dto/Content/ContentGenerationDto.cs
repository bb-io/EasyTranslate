using Apps.EasyTranslate.Models.Dto.Generic;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Content;

public class ContentGenerationDto
{
    [JsonProperty("data")]
    public Data<ContentGenerationAttributes> Data { get; set; }
}