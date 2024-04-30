using Apps.EasyTranslate.Models.Dto.Generic;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Libraries;

public class GetLibraryDto
{
    [JsonProperty("data")]
    public Data<LibraryAttributes> Data { get; set; }
}