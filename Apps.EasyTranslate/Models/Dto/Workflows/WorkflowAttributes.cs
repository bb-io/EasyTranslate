using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Workflows;

public class WorkflowAttributes
{
    [JsonProperty("display_name")]
    public string DisplayName { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
    
    [JsonProperty("identifier")]
    public string Identifier { get; set; }
    
    [JsonProperty("is_predefined")]
    public bool IsPredefined { get; set; }
    
    [JsonProperty("status")]
    public string Status { get; set; }
    
    [JsonProperty("is_available")]
    public bool IsAvailable { get; set; }
    
    [JsonProperty("source_language")]
    public SourceLanguageDto SourceLanguage { get; set; }
    
    [JsonProperty("has_copywriting_steps")]
    public bool HasCopywritingSteps { get; set; }
    
    [JsonProperty("is_machine_translation")]
    public bool IsMachineTranslation { get; set; }
    
    [JsonProperty("is_ai_translation")]
    public bool IsAiTranslation { get; set; }
    
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; set; }
}