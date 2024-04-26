using Apps.EasyTranslate.Models.Dto.Generic;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Libraries;

public class GetAllLibrariesDto
{
    [JsonProperty("data")]
    public Data<LibraryAttributes>[] Data { get; set; }
    
    [JsonProperty("meta")]
    public MetaPagination Meta { get; set; }
}