using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Responses.TranslationStrings;

public class TranslationStringHistoryResponse
{
    public string Text { get; set; }

    public string Agent { get; set; }

    [Display("User name")]
    public string UserName { get; set; }

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }
}
