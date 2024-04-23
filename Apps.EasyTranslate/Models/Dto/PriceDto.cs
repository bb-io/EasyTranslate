using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto;

public class PriceDto
{
    public long Amount { get; set; }
    
    [JsonProperty("amount_euro")]
    public long AmountEuro { get; set; }
    
    public long Total { get; set; }
    
    [JsonProperty("total_euro")]
    public long TotalEuro { get; set; }
    
    public string Currency { get; set; }
}