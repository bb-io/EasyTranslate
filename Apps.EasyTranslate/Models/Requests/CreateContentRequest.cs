using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Requests;

public class CreateContentRequest : TeamRequest
{
    public string Prompt { get; set; }

    [Display("Language", Description = "Language, for example: en, es, fr, etc. By default en")]
    public string? Language { get; set; }

    [Display("Max tokens", Description = "Max tokens, by default 340")]
    public long? MaxTokens { get; set; }
    
    [Display("Temperature", Description = "Temperature, by default 0.5")]
    public string? Temperature { get; set; }

    [Display("Presence penalty", Description = "Presence penalty, by default 0")]
    public string? PresencePenalty { get; set; }
    
    [Display("Frequency penalty", Description = "Frequency penalty, by default 0")]
    public string? FrequencyPenalty { get; set; }

    [Display("Main subject")]
    public string? MainSubject { get; set; }

    [Display("Content brief")]
    public string? ContentBrief { get; set; }

    public IEnumerable<string>? Keywords { get; set; }
    
    [Display("Tone of voice", Description = "Tone of voice, for example: funny, informal, etc.")]
    public IEnumerable<string>? ToneOfVoice { get; set; }
}