using Newtonsoft.Json;

namespace Apps.EasyTranslate.Models.Dto.Libraries;

public class LibraryAutomationAttribute
{
    [JsonProperty("type")]
    public string Type { get; set; }
    
    [JsonProperty("is_enabled")]
    public bool IsEnabled { get; set; }
    
    [JsonProperty("threshold")]
    public int Threshold { get; set; }
    
    [JsonProperty("current")]
    public Dictionary<string, List<CurrentLanguageDto>> Current { get; set; }
}

public class CurrentLanguageDto
{
    [JsonProperty("started_at")]
    public DateTime StartedAt { get; set; }
    
    [JsonProperty("accepted_at")]
    public DateTime? AcceptedAt { get; set; }
    
    [JsonProperty("finished_at")]
    public DateTime? FinishedAt { get; set; }
    
    [JsonProperty("laas_project_id")]
    public string LaasProjectId { get; set; }
    
    [JsonProperty("laas_task_id")]
    public string LaasTaskId { get; set; }
    
    [JsonProperty("keys")]
    public List<string> Keys { get; set; }
}