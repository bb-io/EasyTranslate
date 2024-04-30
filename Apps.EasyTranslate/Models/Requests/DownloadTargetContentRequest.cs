using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Requests;

public class DownloadTargetContentRequest
{
    [Display("Content URL")]
    public string ContentUrl { get; set; }

    [Display("File name")]
    public string? FileName { get; set; }
}
