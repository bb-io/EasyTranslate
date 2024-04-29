using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Requests;

public class DownloadTargetContentRequest
{
    [Display("Target content URL")]
    public string TargetContentUrl { get; set; }

    [Display("File name")]
    public string? FileName { get; set; }
}
