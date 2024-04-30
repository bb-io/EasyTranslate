using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Webhooks.Models.Responses;

public class TranslationResponse
{
    [Display("Translation ID")]
    public string Id { get; set; }
    
    public string Key { get; set; }
    
    public string Text { get; set; }
    
    public string Status { get; set; }
    
    [Display("Segment ID")]
    public int SegmentId { get; set; }
    
    [Display("Language code")]
    public string LanguageCode { get; set; }
    
    [Display("Library ID")]
    public string LibraryId { get; set; }
}