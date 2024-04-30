using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.TranslationStrings;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Responses.TranslationStrings;

public class TranslationStringResponse
{
    [Display("Translation string ID")]
    public string Id { get; set; }

    [Display("Text")]
    public string Text { get; set; }

    [Display("Status")]
    public string Status { get; set; }

    public string Key { get; set; }

    [Display("Translation key ID")]
    public string KeyId { get; set; }

    [Display("Library ID")]
    public string LibraryId { get; set; }

    [Display("Language code")]
    public string LanguageCode { get; set; }

    [Display("History")]
    public List<TranslationStringHistoryResponse> History { get; set; }

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }

    public TranslationStringResponse(Data<TranslationStringAttributes> data)
    {
        Id = data.Id;
        Key = data.Attributes.Key;
        Text = data.Attributes.Text;
        Status = data.Attributes.Status;
        KeyId = data.Attributes.KeyId;
        LibraryId = data.Attributes.LibraryId;
        LanguageCode = data.Attributes.LanguageCode;
        History = data.Attributes.History.Select(h => new TranslationStringHistoryResponse()
        {
            Text = h.Text,
            Agent = h.Agent,
            UserName = h.UserName,
            CreatedAt = h.CreatedAt,
            UpdatedAt = h.UpdatedAt
        }).ToList();
        CreatedAt = data.Attributes.CreatedAt;
        UpdatedAt = data.Attributes.UpdatedAt;
    }
}
