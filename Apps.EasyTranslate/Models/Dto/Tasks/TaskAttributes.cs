using Apps.EasyTranslate.Utils;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Tasks;

public class TaskAttributes
{
    [JsonProperty("target_content")]
    public string TargetContent { get; set; } = string.Empty;
    
    [JsonProperty("target_language")]
    public string TargetLanguage { get; set; }
    
    [JsonProperty("source_language")]
    public string SourceLanguage { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("file_name")]
    public string FileName { get; set; }

    [JsonProperty("word_count"), JsonConverter(typeof(NullToDefaultConverter<long>), 0)]
    public long WordCount { get; set; }
    
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("price")]
    public PriceDto Price { get; set; }

    [JsonProperty("progress")]
    public string Progress { get; set; }

    [JsonProperty("is_rated")]
    public bool IsRated { get; set; }
    
    [JsonProperty("is_content"), JsonConverter(typeof(NullToDefaultConverter<bool>), false)]
    public bool IsContent { get; set; }

    [JsonProperty("revision_status")]
    public string RevisionStatus { get; set; }
    
    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }
    
    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; }
}