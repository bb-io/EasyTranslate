using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Keys;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Responses.Keys;

public class TranslationKeyResponse
{
    [Display("Key ID")]
    public string Id { get; set; }
    
    public string Name { get; set; }

    [Display("Library ID")]
    public string LibraryId { get; set; }

    public List<TranslationResponse> Translations { get; set; }

    [Display("Created At")]
    public DateTime CreatedAt { get; set; }

    [Display("Updated At")]
    public DateTime UpdatedAt { get; set; }

    public TranslationKeyResponse(Data<TranslationKeyAttributes> data)
    {
        Id = data.Id;
        Name = data.Attributes.Name;
        LibraryId = data.Attributes.LibraryId;
        Translations = data.Attributes.Translations.Select(x => new TranslationResponse
        {
            Id = x.Id,
            Text = x.Attributes.Text,
            Status = x.Attributes.Status,
            LanguageCode = x.Attributes.LanguageCode
        }).ToList();
        CreatedAt = DateTime.Parse(data.Attributes.CreatedAt);
        UpdatedAt = DateTime.Parse(data.Attributes.UpdatedAt);
    }
}
