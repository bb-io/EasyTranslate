using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Libraries;

public class LibraryAttributes
{
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("type")]
    public string Type { get; set; }
    
    [JsonProperty("file_name")]
    public string FileName { get; set; }
    
    [JsonProperty("status")]
    public string Status { get; set; }
    
    [JsonProperty("team_id")]
    public string TeamId { get; set; }
    
    [JsonProperty("user")]
    public UserLibrary User { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
    
    [JsonProperty("source_language")]
    public string SourceLanguage { get; set; }
    
    [JsonProperty("languages")]
    public string[] Languages { get; set; }
    
    [JsonProperty("overview")]
    public Overview[] Overview { get; set; }
    
    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }
    
    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; }
}