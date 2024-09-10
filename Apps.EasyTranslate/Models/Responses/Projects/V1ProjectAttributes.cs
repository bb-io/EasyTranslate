using Apps.EasyTranslate.Models.Dto;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Responses.Projects;

public class V1ProjectAttributes
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("source_content")]
    public string SourceContent { get; set; }
    
    [JsonProperty("source_language")]
    public string SourceLanguage { get; set; }
    
    [JsonProperty("file_name")]
    public string FileName { get; set; }
    
    [JsonProperty("target_languages")]
    public string[] TargetLanguages { get; set; }

    [JsonProperty("price")]
    public PriceDto Price { get; set; }
    
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("progress")]
    public ProgressDto Progress { get; set; }

    [JsonProperty("words_count")]
    public long WordsCount { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
    
    [JsonProperty("workflow")]
    public string Workflow { get; set; } = string.Empty;
}