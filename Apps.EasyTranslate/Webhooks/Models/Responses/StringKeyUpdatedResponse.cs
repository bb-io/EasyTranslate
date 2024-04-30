using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Webhooks.Models.Payload.StringKeyUpdated;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Webhooks.Models.Responses;

public class StringKeyUpdatedResponse
{
    [Display("String Key ID")]
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    [Display("Library ID")]
    public string LibraryId { get; set; }
    
    public List<TranslationResponse> Translations { get; set; }

    public StringKeyUpdatedResponse(Data<StringKeyUpdatedAttributes> data)
    {
        Id = data.Id;
        Name = data.Attributes.Name;
        LibraryId = data.Attributes.LibraryId;
        Translations = data.Attributes.Translations.Select(t => new TranslationResponse
        {
            Id = t.Id,
            Key = t.Key,
            Text = t.Text,
            Status = t.Status,
            SegmentId = t.SegmentId,
            LanguageCode = t.LanguageCode,
            LibraryId = t.LibraryId
        }).ToList();
    }
}