using Apps.EasyTranslate.Models.Dto;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Webhooks.Models.Payload.TaskUpdated;

public class TaskUpdatedAttributes
{
    [JsonProperty("target_content")]
    public string TargetContent { get; set; }

    [JsonProperty("target_language")]
    public string TargetLanguage { get; set; }

    [JsonProperty("source_language")]
    public string SourceLanguage { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("file_name")]
    public string FileName { get; set; }

    [JsonProperty("word_count")]
    public int WordCount { get; set; }

    [JsonProperty("deadline")]
    public string? Deadline { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("price")]
    public PriceDto Price { get; set; }

    [JsonProperty("progress")]
    public int Progress { get; set; }

    [JsonProperty("is_rated")]
    public bool IsRated { get; set; }

    [JsonProperty("is_content")]
    public bool IsContent { get; set; }

    [JsonProperty("order")]
    public int Order { get; set; }

    [JsonProperty("supplier_id")]
    public string SupplierId { get; set; }

    //[JsonProperty("project_id")]
    //public string ProjectId { get; set; }

    [JsonProperty("project")]
    public ProjectObject? Project { get; set; }

    [JsonProperty("revision_status")]
    public string RevisionStatus { get; set; }

    [JsonProperty("string_library_id")]
    public object StringLibraryId { get; set; }

    [JsonProperty("created_at")]
    public string CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; }
}

public class ProjectObject
{
    [JsonProperty("id")]
    public string Id { get; set; }
}