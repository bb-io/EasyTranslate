using Apps.EasyTranslate.Models.Dto.Generic;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Libraries;

public class LibraryAutomationDto
{
    [JsonProperty("data")]
    public Data<LibraryAutomationAttribute> Data { get; set; }
}