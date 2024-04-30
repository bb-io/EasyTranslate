using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.EasyTranslate.Models.Requests;

public class CreateProjectFromFileRequest : CreateProjectRequest
{
    public IEnumerable<FileReference> Files { get; set; }

    public DateTime? Deadline { get; set; }

    [Display("Callback URL")]
    public string? CallbackUrl { get; set; }

    [Display("Folder name")]
    public string? FolderName { get; set; }

    [Display("Folder ID")]
    public string? FolderId { get; set; }
}