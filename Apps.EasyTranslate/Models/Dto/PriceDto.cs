using Apps.EasyTranslate.Utils;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto;

public class PriceDto
{    
    [JsonConverter(typeof(NullToDefaultConverter<long>), 0)]
    public long Amount { get; set; }
    
    [JsonProperty("amount_euro"), JsonConverter(typeof(NullToDefaultConverter<long>), 0)]
    public long AmountEuro { get; set; }
    
    [JsonConverter(typeof(NullToDefaultConverter<long>), 0)]
    public long Total { get; set; }
    
    [JsonProperty("total_euro"), JsonConverter(typeof(NullToDefaultConverter<long>), 0)]
    public long TotalEuro { get; set; }
    
    public string Currency { get; set; }
}